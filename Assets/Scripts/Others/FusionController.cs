using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FusionController : MonoBehaviour
{
    public static FusionController Instance;


    public GameObject player1;
    public GameObject player2;

    private bool isFusion = false;
    private bool hasJumped = false;
    private GameObject fusionLeader;
    private GameObject fusionFollower;

    private Vector2 originPos;


    private int originalWorldId = 0;


    private void Awake()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        Instance = this;
    }

    public void StartFusion(GameObject leader, GameObject follower)
    {
        isFusion = true;
        fusionLeader = leader;
        fusionFollower = follower;
        GoNearBy();
        AttributeImproved(leader,follower);
        WeaponImproved(leader, follower);
        // 禁用follower的输入控制
        if (follower.TryGetComponent<PlayerControllerForPlayer1>(out var control1))
        {
            control1.OnDisable();
        }

        else if(follower.TryGetComponent<PlayerControllerForPlayer2>(out var control2))
        {
            control2.OnDisable();
        }
        //修改玩家currentWorld:被跳跃的一方修改为跳跃的一方
        PlayerStatus status_leader = leader.GetComponent<PlayerStatus>();
        PlayerStatus status_follower = follower.GetComponent<PlayerStatus>();
        originalWorldId = status_follower.currentWorld;
        status_follower.currentWorld = status_leader.currentWorld;

    }

    //武器强化
    /*
     * 获取leader 的weaponHolder下的所有武器
     * 替换为强化武器（打开基础武器的强化标记)
     * 禁用follower 的weaponHolder
     *
     */
    private void WeaponImproved(GameObject leader,GameObject follower)
    {
        follower.transform.Find("WeaponHolder").gameObject.SetActive(false);
        if (leader.name == "Player1")
        {
            MeleeWeapon[] weapons = leader.transform.Find("WeaponHolder").GetComponentsInChildren<MeleeWeapon>();
            foreach (var weapon in weapons) {
                weapon.isImproved = true;
            }
        }
        else if (leader.name == "Player2")
        {
            RemoteWeapon[] weapons = leader.transform.Find("WeaponHolder").GetComponentsInChildren<RemoteWeapon>();
            foreach (var weapon in weapons)
            {
                weapon.isImproved = true;
            }
        }
    }

    //取消强化（武器）
    private void WeaponDeclared(GameObject leader, GameObject follower)
    {
        follower.transform.Find("WeaponHolder").gameObject.SetActive(true);
        if (leader.name == "Player1")
        {
            MeleeWeapon[] weapons = leader.transform.Find("WeaponHolder").GetComponentsInChildren<MeleeWeapon>();
            foreach (var weapon in weapons)
            {
                weapon.isImproved = false;
            }
        }
        else if (leader.name == "Player2")
        {
            RemoteWeapon[] weapons = leader.transform.Find("WeaponHolder").GetComponentsInChildren<RemoteWeapon>();
            foreach (var weapon in weapons)
            {
                weapon.isImproved = false;
            }
        }
    }

    //属性强化
    private void AttributeImproved(GameObject leader, GameObject follower)
    {
        PlayerStatus status1 = leader.GetComponent<PlayerStatus>();
        PlayerStatus status2 = follower.GetComponent<PlayerStatus>();
        if(status1 && status2)
        {
            status1.attackForce += status2.attackForce;
            status1.defenceForce += status2.defenceForce;
        }
    }

    //取消强化
    private void AttributeDeclared(GameObject leader, GameObject follower)
    {
        PlayerStatus status1 = leader.GetComponent<PlayerStatus>();
        PlayerStatus status2 = follower.GetComponent<PlayerStatus>();
        if (status1 && status2)
        {
            status1.attackForce -= status2.attackForce;
            status1.defenceForce -= status2.defenceForce;
        }
    }

    public void StopFusion()
    {
        isFusion = false;
        hasJumped = false;
        if (fusionFollower.TryGetComponent<PlayerControllerForPlayer1>(out var control1))
        {
            control1.OnEnable();
        }

        else if (fusionFollower.TryGetComponent<PlayerControllerForPlayer2>(out var control2))
        {
            control2.OnEnable();
        }
        AttributeDeclared(fusionLeader,fusionFollower);
        WeaponDeclared(fusionLeader, fusionFollower);
        SceneController.instance.ShiftToCentre();
       

        PlayerStatus status_follower = fusionFollower.GetComponent<PlayerStatus>();
         status_follower.currentWorld = originalWorldId;
        fusionLeader = null;
        fusionFollower = null;
    }

    void FixedUpdate()
    {
        
        if (isFusion && fusionLeader != null && fusionFollower != null)
        {

            if (!hasJumped)
            {
                GetIntoFollowerWorld();
            }
            // 简单跟随方式：强制对齐位置
            Follow();
        }
    }

    public void GetIntoFollowerWorld()
    {
        ReturnToPosition();
        if (fusionLeader.name == "Player1")
        {
            Debug.Log("主导者是player1，进入Player2的遮罩");
            StartCoroutine(SceneController.instance.ShiftToWorld1());
        }
        else if (fusionLeader.name == "Player2")
        {
            //主导者是player2，进入Player1的遮罩
            StartCoroutine(SceneController.instance.ShiftToWorld2());

        }
        hasJumped = true;
    }

    public void ReturnToLeaderWorld()
    {
        fusionLeader.transform.position = originPos;
        StartCoroutine(SceneController.instance.ShiftToCentre());


    }

    //前往对方世界的移动函数，待实现具体跳跃特效
    public void GoNearBy()
    {
        originPos = fusionLeader.transform.position;
    }


    //返回自己世界的移动函数，待实现具体返回特效
    public void ReturnToPosition()
    {
        fusionLeader.transform.position = fusionFollower.transform.position;
    }

    public void Follow()
    {
        Vector3 offset = new Vector3(1.5f, 0, 0); // 保持一定间距
        fusionFollower.transform.position = Vector3.Lerp(
            fusionFollower.transform.position,
            fusionLeader.transform.position + offset,
            10f * Time.fixedDeltaTime
        );
    }


    public void GoBack()
    {
        ReturnToLeaderWorld();
        StopFusion();

    }
}


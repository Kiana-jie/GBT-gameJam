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
    private Vector2 originPos_2;


    private int originalWorldId = 0;


    private void Awake()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        Instance = this;
    }

    void FixedUpdate()
    {

        if (isFusion && fusionLeader != null && fusionFollower != null)
        {

            if (!hasJumped)
            {
                GetIntoFollowerWorld();
            }
            //// �򵥸��淽ʽ��ǿ�ƶ���λ��
            Follow();
        }
    }

    public void StartFusion(GameObject leader, GameObject follower)
    {
        isFusion = true;
        fusionLeader = leader;
        fusionFollower = follower;
        GoNearBy();
        //GetIntoFollowerWorld();
        AttributeImproved(leader,follower);
        WeaponImproved(leader, follower);
        // ����follower���������
        if (follower.TryGetComponent<PlayerControllerForPlayer1>(out var control1))
        {
            control1.OnDisable();
        }

        else if(follower.TryGetComponent<PlayerControllerForPlayer2>(out var control2))
        {
            control2.OnDisable();
        }
        //�޸����currentWorld:����Ծ��һ���޸�Ϊ��Ծ��һ��
        PlayerStatus status_leader = leader.GetComponent<PlayerStatus>();
        PlayerStatus status_follower = follower.GetComponent<PlayerStatus>();
        originalWorldId = status_follower.currentWorld;
        status_follower.currentWorld = status_leader.currentWorld;
        
    }

    //����ǿ��
    /*
     * ��ȡleader ��weaponHolder�µ���������
     * �滻Ϊǿ���������򿪻���������ǿ�����)
     * ����follower ��weaponHolder
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

    //ȡ��ǿ����������
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

    //����ǿ��
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

    //ȡ��ǿ��
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
        ReturnToLeaderWorld();
        GoBack();

        PlayerStatus status_follower = fusionFollower.GetComponent<PlayerStatus>();
         status_follower.currentWorld = originalWorldId;
        
        fusionLeader = null;
        fusionFollower = null;
    }

    

    public void GetIntoFollowerWorld()
    {
        
        if (fusionLeader.name == "Player1")
        {
            Debug.Log("��������player1������Player2������");
            StartCoroutine(SceneController.instance.ShiftToWorld1());
        }
        else if (fusionLeader.name == "Player2")
        {
            //��������player2������Player1������
            StartCoroutine(SceneController.instance.ShiftToWorld2());

        }
        hasJumped = true;
    }

    public void ReturnToLeaderWorld()
    {
        
        StartCoroutine(SceneController.instance.ShiftToCentre());
        hasJumped = false;


    }

    //ǰ���Է�������ƶ���������ʵ�־�����Ծ��Ч
    public void GoNearBy()
    {
        //�����ֵ�ַ
        originPos = fusionLeader.transform.position;
        originPos_2 = fusionFollower.transform.position;
        //����
        fusionLeader.transform.position = fusionFollower.transform.position;
    }


    //�����Լ�������ƶ���������ʵ�־��巵����Ч
    public void GoBack()
    {
        fusionLeader.transform.position = originPos;
        fusionFollower.transform.position = originPos_2;
    }

    public void Follow()
    {
        Vector3 offset = new Vector3(2f, 0, 0); // ����һ�����
        fusionFollower.transform.position = Vector3.Lerp(
            fusionFollower.transform.position,
            fusionLeader.transform.position + offset,
            10f * Time.fixedDeltaTime
        );
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        Instance = this;
    }

    public void StartFusion(GameObject leader, GameObject follower)
    {
        isFusion = true;
        fusionLeader = leader;
        fusionFollower = follower;
        GoNearBy();

        // ����follower���������
        if (follower.TryGetComponent<PlayerControllerForPlayer1>(out var control1))
        {
            control1.OnDisable();
        }

        else if(follower.TryGetComponent<PlayerControllerForPlayer2>(out var control2))
        {
            control2.OnDisable();
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
        
        SceneController.instance.ShiftToCentre();
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
            // �򵥸��淽ʽ��ǿ�ƶ���λ��
            Follow();
        }
    }

    public void GetIntoFollowerWorld()
    {
        ReturnToPosition();
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
        fusionLeader.transform.position = originPos;
        StartCoroutine(SceneController.instance.ShiftToCentre());


    }

    //ǰ���Է�������ƶ���������ʵ�־�����Ծ��Ч
    public void GoNearBy()
    {
        originPos = fusionLeader.transform.position;
    }


    //�����Լ�������ƶ���������ʵ�־��巵����Ч
    public void ReturnToPosition()
    {
        fusionLeader.transform.position = fusionFollower.transform.position;
    }

    public void Follow()
    {
        Vector3 offset = new Vector3(1.5f, 0, 0); // ����һ�����
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


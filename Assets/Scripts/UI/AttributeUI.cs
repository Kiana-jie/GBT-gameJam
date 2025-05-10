using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class AttributeUI : MonoBehaviour
{
    public static AttributeUI Instance;
    private PlayerStatus status1;
    private PlayerStatus status2;

    public TextMeshProUGUI Dfc1;
    public TextMeshProUGUI Dfc2;

    public TextMeshProUGUI Atk1;
    public TextMeshProUGUI Atk2;

    public TextMeshProUGUI Health1;
    public TextMeshProUGUI Health2;

    private void Awake()
    {
        Instance = this;
        status1 = GameObject.Find("Player1").GetComponent<PlayerStatus>();
        status2 = GameObject.Find("Player2").GetComponent<PlayerStatus>();

    }


    public void AttributeUIUpdate()
    {
        Dfc1.text = status1.defenceForce.ToString();
        Atk1.text = status1.attackForce.ToString();
        Health1.text = status1.maxHealth.ToString();

        Dfc2.text = status2.defenceForce.ToString();
        Atk2.text = status2.attackForce.ToString();
        Health2.text = status2.maxHealth.ToString();

    }
}

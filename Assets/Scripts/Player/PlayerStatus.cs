using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerStatus : MonoBehaviour
{
    [Header("��������")]
    public int currentLevel = 1; //��ǰ�ȼ�
    public int currentExp = 0; //��ǰ����ֵ
    public int neededExp = 15; //�������辭��ֵ
    public int health;
    public int maxHealth;
    public int attackForce;
    public int defenceForce;
    public float speed;
    public float attackSpeed;
    public float attackRate;
    [Header("ʰȡ")]
    public float pickUpRange;
    public int money;
    

    
    private void Awake()
    {
        health = maxHealth;
    }

    private void Start()
    {
        PlayerInfo.Instance.HPUpdate();
        PlayerInfo.Instance.MoneyUpdate();
        PlayerInfo.Instance.ExpUpdate();
    }

    private void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        PlayerInfo.Instance.HPUpdate();
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    //��ֵ���㣺��ɫ�������� + �ӳ� + ��������
  
    //����
    public void LevelUp()
    {
        if(currentExp >= neededExp)
        {
            currentLevel += 1;
            currentExp = 0;
            neededExp += 5 * currentLevel;
            PlayerInfo.Instance.ExpUpdate();
            PlayerInfo.Instance.LevelUpdate();
        }
    }

    //��Ǯ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("money"))
        {
            money++;
            Destroy(collision.gameObject);
            PlayerInfo.Instance.MoneyUpdate();
        }
    }


}

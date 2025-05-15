using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerStatus : MonoBehaviour
{
    [Header("基本属性")]
    public int currentLevel = 1; //当前等级
    public int currentExp = 0; //当前经验值
    public int neededExp = 15; //升级所需经验值
    public int health;
    public int maxHealth;
    public int attackForce;
    public int defenceForce;
    public float speed;
    public float attackSpeed;
    public float attackRate;
    [Header("拾取")]
    public float pickUpRange;
    public int money;
    [Header("所处世界")]
    [Range(1, 2)]
    public int currentWorld;

    
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
        health -= (damage - defenceForce > 0 ? damage - defenceForce : 0);
        StartCoroutine(AttackedToRed());
        AudioManager.Instance.Play("attacked", gameObject);
        PlayerInfo.Instance.HPUpdate();

        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
        GameManager.Instance.GameOver();
    }
    //数值结算：角色自身属性 + 加成 + 武器属性
  
    //升级
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

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //捡钱
        if (collision.CompareTag("money"))
        {
            money++;
            Destroy(collision.gameObject);
            PlayerInfo.Instance.MoneyUpdate();
        }
        //捡能量豆
        if (collision.CompareTag("energyBean"))
        {
            EnergyManager.Instance.AddEnergy();
            Destroy(collision.gameObject);
        }
    }

    IEnumerator AttackedToRed()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Color origin = Color.white;
        Color AttackedCol = Color.red;

        sr.color = AttackedCol;
        yield return new WaitForSeconds(0.15f);

        sr.color = origin;

    }
    


}

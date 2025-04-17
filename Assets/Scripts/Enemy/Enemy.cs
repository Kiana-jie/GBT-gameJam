using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Enemy:MonoBehaviour
{
    public enum EnemyType
    {
        Melee,
        Ranged
    }
    public int health;
    public int damage;
    public float speed;
    public float attackRange;

    private GameObject []players;
    

    protected Transform target;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        SearchTarget();
    }

    private void FixedUpdate()
    {
        
        if(Vector2.Distance(transform.position, target.position) <= attackRange)
        {
            HandleAttack();
        }
        else
        {
            Move(target.position);
        }
        
    }
    /*
     ׷����
    1.���У������λ��һ���������䣩
    2.�ƶ� (���빥����Χ ֹͣ�ƶ���
    3.������������ʵ�־���Ĺ����������˴�Ϊ���󷽷���
     */

    //ִֻ��һ��
    public void SearchTarget()
    {
       if(players.Length == 0) { return; }
       if(players.Length == 1) { target = players[0].transform; return; }

        float dis1 = Vector2.Distance(transform.position, players[0].transform.position);
        float dis2 = Vector2.Distance(transform.position, players[1].transform.position);
        target = (dis1 <= dis2 ? players[0].transform : players[1].transform);    
    }

    public void Move(Vector2 targetPos)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    public void HandleAttack()
    {
        Attack();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{name} took {damage} damage. Remaining: {health}");
        if (health <= 0) 
        {
            Die();
        }
    }

    public void Die()
    {
       Destroy(gameObject);
    }
    public abstract void Attack();
    
}

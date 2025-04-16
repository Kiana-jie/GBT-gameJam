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

    public Transform player1;
    public Transform player2;

    protected Transform target;

    private void Start()
    {
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
        if(player1 == null || player2 == null ) { return; }
        float dis1 = Vector2.Distance(transform.position, player1.position);
        float dis2 = Vector2.Distance(transform.position, player2.position);
        target = (dis1 <= dis2 ? player1 : player2);    
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

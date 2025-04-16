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
    /*
     ׷����
    1.���У������λ��һ���������䣩
    2.�ƶ� (���빥����Χ ֹͣ�ƶ���
    3.������������ʵ�־���Ĺ����������˴�Ϊ���󷽷���
     */

    
    public void SearchTarget(LayerMask layerMask)
    {
        
    }

    public void Move(Vector2 targetPos)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    public void HandleAttack()
    {
        //���빥����Χ������
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

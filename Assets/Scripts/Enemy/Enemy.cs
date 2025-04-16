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
     追击：
    1.索敌（最近单位，一旦锁定不变）
    2.移动 (进入攻击范围 停止移动）
    3.攻击（子类中实现具体的攻击方法，此处为抽象方法）
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
        //进入攻击范围，攻击
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

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
    private Rigidbody2D rb;

    protected Transform target;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        SearchTarget();
        rb = gameObject.GetComponent<Rigidbody2D>();
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

        if(rb.velocity != Vector2.zero)
        {
            RecoverSpeed();
               
        }
        
    }
    /*
     追击：
    1.索敌（最近单位，一旦锁定不变）
    2.移动 (进入攻击范围 停止移动）
    3.攻击（子类中实现具体的攻击方法，此处为抽象方法）
     */

    //只执行一次
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
    
    public void RecoverSpeed()
    {
        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, 0.1f), Mathf.Lerp(rb.velocity.y, 0, 0.1f));
    }
}

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
    public float attackRange; //范围
    public float attackTime;    //攻击间隔
    public float attackTimer = 0;   //计时器
    public bool isCooling = false;      //攻击冷却
    public int providedExp = 1;

    protected GameObject []players;
    protected Rigidbody2D rb;

    protected Vector2 target;

    public GameObject moneyPrefab;

    private PlayerStatus lastAttacker;

    

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        SearchTarget();
        rb = gameObject.GetComponent<Rigidbody2D>();
        moneyPrefab = (GameObject)Resources.Load("Prefabs/money");
    }

    private void FixedUpdate()
    {
        SearchTarget();

        if (Vector2.Distance(transform.position, target) >= attackRange)
        {
            Move(target);
        }

        if(rb.velocity != Vector2.zero)
        {
            RecoverSpeed();
               
        }
        
    }

    private void Update()
    {
        if(isCooling)
        {
            attackTimer -= Time.deltaTime;
            if(attackTimer <= 0)
            {
                attackTimer = 0;
                isCooling = false;
            }
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
       if(players.Length == 1) { target = players[0].transform.position; return; }

        float dis1 = Vector2.Distance(transform.position, players[0].transform.position);
        float dis2 = Vector2.Distance(transform.position, players[1].transform.position);
        target = (dis1 <= dis2 ? players[0].transform.position : players[1].transform.position);    
    }

    public void Move(Vector2 targetPos)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        TurnAround();
    }

    public abstract void HandleAttack();
    

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
        
       GameObject.Instantiate(moneyPrefab, transform.position, Quaternion.identity);

        //提供经验
        foreach(var player in players)
        {
            PlayerStatus status = player.GetComponent<PlayerStatus>();
            status.currentExp += providedExp;
            status.LevelUp();
            PlayerInfo.Instance.ExpUpdate();
        }
       Destroy(gameObject);
    }
    public abstract void Attack(GameObject target);
    
    public void RecoverSpeed()
    {
        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, 0.1f), Mathf.Lerp(rb.velocity.y, 0, 0.1f));
    }

    //自动转向
    public void TurnAround()
    {
        //玩家在怪物右边
        if(target.x - transform.position.x >= 0 )
        {
            //向右
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
        } else if(target.x - transform.position.x < 0 )
            //向左
        {
            transform.localScale = new Vector3( -Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}

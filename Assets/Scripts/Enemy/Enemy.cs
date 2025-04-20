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
    public float attackRange; //��Χ
    public float attackTime;    //�������
    public float attackTimer = 0;   //��ʱ��
    public bool isCooling = false;      //������ȴ
    public int providedExp = 1;

    private GameObject []players;
    private Rigidbody2D rb;

    protected Transform target;

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
        
        if(Vector2.Distance(transform.position, target.position) >= attackRange)
        {
            Move(target.position);
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

        //�ṩ����
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

    //�Զ�ת��
    public void TurnAround()
    {
        //����ڹ����ұ�
        if(target.transform.position.x - transform.position.x >= 0 )
        {
            //����
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
        } else if(target.transform.position.x - transform.position.x < 0 )
            //����
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}

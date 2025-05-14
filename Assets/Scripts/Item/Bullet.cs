using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f;
    public float speed = 5f;
    public Vector3 targetDir;
    public int damage;
    // Start is called before the first frame update

    private PlayerStatus player2;

    private void Awake()
    {
        player2 = GameObject.Find("Player2").GetComponent<PlayerStatus>();
    }

    void Start()
    {
         if(gameObject != null)
         {
            Destroy(gameObject,lifeTime);
         }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(targetDir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //近战敌人
        if (collision.CompareTag("Enemy"))
        {
            // 伤害
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage + player2.attackForce);
            }
            Destroy(gameObject);
        }
        //特殊敌人
        if (collision.CompareTag("SpecialEnemy"))
        {
            Enemy_2 enemy = collision.GetComponent<Enemy_2>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage + player2.attackForce);
            }
            Destroy(gameObject);
        }

    }
}

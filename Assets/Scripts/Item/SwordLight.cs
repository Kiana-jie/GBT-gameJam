using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordLight : MonoBehaviour
{

    //�Ż������⽥����Ч
    public float lifeTime;
    public float speed;
    public int damage;
    public Vector2 targetDir;

    void Start()
    {
        if (gameObject != null)
            Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(targetDir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //��ս����
        if (collision.CompareTag("Enemy"))
        {
            // �˺�
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            
        }
        //�������
        if (collision.CompareTag("SpecialEnemy"))
        {
            Enemy_2 enemy = collision.GetComponent<Enemy_2>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            
        }
    }
}

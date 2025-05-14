using System.Collections;
using UnityEngine;

public class WeaponHitBox : MonoBehaviour
{
    public int damage = 10;
    public float knockbackForce = 5f;
    public float recoverTime = 0.01f;
    private PlayerStatus player1;

    private void Awake()
    {
        player1 = GameObject.Find("Player1").GetComponent<PlayerStatus>();
    }
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // �˺�
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage + player1.attackForce);
            }

            // ���˷��� = ����λ�� - ����λ��
            Vector2 knockDir = (other.transform.position - transform.position).normalized;
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(knockDir * knockbackForce, ForceMode2D.Impulse);
               
            }

            
        }
        if (other.CompareTag("SpecialEnemy"))
        {
            // �˺�
            Enemy_2 enemy = other.GetComponent<Enemy_2>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage + player1.attackForce);
            }

            // ���˷��� = ����λ�� - ����λ��
            Vector2 knockDir = (other.transform.position - transform.position).normalized;
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(knockDir * knockbackForce, ForceMode2D.Impulse);

            }


        }


    }

    
}

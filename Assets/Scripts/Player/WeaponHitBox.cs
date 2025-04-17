using UnityEngine;

public class WeaponHitBox : MonoBehaviour
{
    public int damage = 10;
    public float knockbackForce = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // �˺�
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
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

using UnityEngine;

public class WeaponHitBox : MonoBehaviour
{
    public int damage = 10;
    public float knockbackForce = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // 伤害
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // 击退方向 = 敌人位置 - 武器位置
            Vector2 knockDir = (other.transform.position - transform.position).normalized;
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(knockDir * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}

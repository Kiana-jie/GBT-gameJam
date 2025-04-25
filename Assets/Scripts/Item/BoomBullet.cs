using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;

public class BoomBullet : MonoBehaviour
{
    public float lifeTime = 3f;
    public float speed = 5f;
    public Vector3 targetDir;
    public int damage;
    public float boomRadius;
    //public GameObject boomPrefab;//�ɲ�����������Ч��ʵ��?
    // Start is called before the first frame update
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

    //���б�ը
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //��ս����
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Boom!");
            Boom();
            
        }
        //�������
        if (collision.CompareTag("SpecialEnemy"))
        {
            Debug.Log("Boom!");
            Boom();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, boomRadius);
    }

    public void Boom()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, boomRadius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Enemy_1 enemy = collider.GetComponent<Enemy_1>();
                enemy.TakeDamage(damage);
            }
            else if (collider.CompareTag("SpecialEnemy"))
            {
                Enemy_2 enemy = collider.GetComponent<Enemy_2>();
                enemy.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}

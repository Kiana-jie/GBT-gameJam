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
    void Start()
    {
         if(gameObject != null)
        Destroy(gameObject,lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(targetDir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // …À∫¶
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}

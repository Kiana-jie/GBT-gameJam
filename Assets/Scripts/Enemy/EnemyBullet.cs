using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBullet : MonoBehaviour
{
    public float lifeTime = 3f;
    public float speed = 2.5f;
    public Vector3 targetDir;
    public int damage;

    void Start()
    {
        if (gameObject != null)
        {
            Destroy(gameObject, lifeTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(targetDir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            // …À∫¶
            PlayerStatus player = collision.GetComponent<PlayerStatus>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            Destroy(gameObject);
        }

    }
}

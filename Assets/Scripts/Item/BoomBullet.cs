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
    public GameObject boomPrefab;//�ɲ�����������Ч��ʵ��?
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
            Destroy(gameObject);
        }
        //�������
        if (collision.CompareTag("SpecialEnemy"))
        {
            Debug.Log("Boom!");
            Destroy(gameObject);
        }
    }
}

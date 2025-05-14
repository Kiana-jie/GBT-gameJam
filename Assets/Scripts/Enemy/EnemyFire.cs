using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject bullet;
    private float fireTimer = 0;
    public float cooldown;
    public int fireNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;
        if( fireTimer > cooldown )
        {
            Fire();
            fireTimer = 0;
        }
    }

    //开火，朝周围方向射出n个子弹
    public void Fire()
    {
        for (int i = 1; i <= fireNum; i++)
        {
            //计算子弹发射角度
            float angle = 360f / fireNum * i;
            float rad = angle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
            Vector3 spawnPos = transform.position + offset;

            //发射
              GameObject.Instantiate(bullet, spawnPos, Quaternion.identity);
            
            bullet.GetComponent<EnemyBullet>().targetDir = spawnPos - transform.position;
        }

    }
}

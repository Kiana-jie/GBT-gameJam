using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemyPool;           // 敌人预制体数组
    public float produceTime = 3f;      // 生成间隔
    private float timer = 0f;

    public Vector2 spawnAreaMin;        // 生成区域左下角
    public Vector2 spawnAreaMax;        // 生成区域右上角

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > produceTime)
        {
            timer = 0;
            Produce();
        }
    }

    public void Produce()
    {
        Vector2 spawnPos = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        GameObject enemy = Instantiate(enemyPool[Random.Range(0, enemyPool.Length)], spawnPos, Quaternion.identity);
    }

    // 可视化生成区域
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = (spawnAreaMin + spawnAreaMax) / 2f;
        Vector3 size = (spawnAreaMax - spawnAreaMin);
        Gizmos.DrawWireCube(center, size);
    }
}

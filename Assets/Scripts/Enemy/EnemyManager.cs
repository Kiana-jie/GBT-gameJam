using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemyPool;           // ����Ԥ��������
    public float produceTime = 3f;      // ���ɼ��
    private float timer = 0f;

    public Vector2 spawnAreaMin;        // �����������½�
    public Vector2 spawnAreaMax;        // �����������Ͻ�

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

    // ���ӻ���������
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = (spawnAreaMin + spawnAreaMax) / 2f;
        Vector3 size = (spawnAreaMax - spawnAreaMin);
        Gizmos.DrawWireCube(center, size);
    }
}

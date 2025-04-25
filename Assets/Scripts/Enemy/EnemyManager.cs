using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public GameObject[] enemyPool;           // 敌人预制体数组
    public GameObject warningPrefab;
    public float produceTime = 3f;      // 生成间隔
    private float timer = 0f;
    private int enemyPerWave;

    public Vector2 spawnAreaMin;        // 生成区域左下角
    public Vector2 spawnAreaMax;        // 生成区域右上角

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > produceTime)
        {
            timer = 0;
            StartCoroutine(Produce());
        }
    }

    IEnumerator Produce()
    {
        enemyPerWave = Random.Range(1, 5);
        for(int i = 0; i < enemyPerWave; i++)
        {
            Vector2 spawnPos = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );
            StartCoroutine(Warning(spawnPos));
            
        }
        yield break;
    }
    IEnumerator Warning(Vector2 spawnPos)
    {
        //给玩家提示信息哪里会出怪
        GameObject warning = Instantiate(warningPrefab, spawnPos, Quaternion.identity);
        SpriteRenderer sr = warning.GetComponent<SpriteRenderer>();

        int flashTimes = 2;
        float fadeTime = 0.5f;

        for (int i = 0; i < flashTimes; i++)
        {
            float timer = 0;
            while (timer < fadeTime)
            {
                float alpha = timer / fadeTime;
                sr.color = new Color(1f, 1f, 1f, alpha);
                timer += Time.deltaTime;
                yield return null;
            }

            sr.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.1f);

            timer = 0;
            while (timer < fadeTime)
            {
                float alpha = 1 - timer / fadeTime;
                sr.color = new Color(1f, 1f, 1f, alpha);
                timer += Time.deltaTime;
                yield return null;
            }

            sr.color = new Color(1f, 1f, 1f, 0);
        }
        Destroy(warning);

        GameObject enemy = Instantiate(enemyPool[Random.Range(0, enemyPool.Length)], spawnPos, Quaternion.identity,gameObject.transform);
    }

    public void DestroyAllEnemies()
    {
        foreach(var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
        foreach(var warning in GameObject.FindGameObjectsWithTag("Warning"))
        {
            Destroy(warning);
        }
        foreach(var se in GameObject.FindGameObjectsWithTag("SpecialEnemy"))
        {
            Destroy(se);
        }
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

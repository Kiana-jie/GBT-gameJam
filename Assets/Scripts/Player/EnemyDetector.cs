using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyDetector : MonoBehaviour
{
    [Header("Ë÷µÐÏà¹ØÊôÐÔ")]
    public float detectionRadius = 5f;
    public LayerMask enemyLayer;
    public Transform currentTarget;

    public void SearchTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);

        if (hits.Length > 0)
        {
            currentTarget = hits
                .OrderBy(hit => Vector2.Distance(transform.position, hit.transform.position))
                .First()
                .transform;
        }

        else
        {
            currentTarget = null;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
     void Update()
    {
        SearchTarget();
    }
}

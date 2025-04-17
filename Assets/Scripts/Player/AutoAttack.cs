using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public EnemyDetector detector;
    public float attackCooldown = 1f;
    public float attackRange = 1.5f;
    public int damage = 10;
    private float attackTimer;

    public Transform weaponTransform;
    public float stabDistance = 1f;
    public float stabDuration = 0.1f;

    void Update()
    {
        
    }

    void Attack()
    {
        

    }

    
}

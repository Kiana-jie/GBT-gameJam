using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    public EnemyType type = EnemyType.Melee;//近战
    
     public override void Attack(GameObject target)
     {
        if (isCooling) { return; }
        PlayerStatus status = target.GetComponent<PlayerStatus>();
        
        if (status != null)
        {
            Debug.Log("attacked!");
            status.TakeDamage(damage);
            isCooling = true;
            attackTimer = attackTime;
        }
    }

    public override void HandleAttack()
    {
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        
        if(other.CompareTag("Player"))
        {
            //Debug.Log("!");
            Attack(other.gameObject);
            //冷却
            

        }
        //攻击效果：击退、等
        //else Debug.Log("!");
    }

   
}

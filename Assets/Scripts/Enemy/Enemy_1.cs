using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    public EnemyType type = EnemyType.Melee;//��ս
    
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
            //��ȴ
            

        }
        //����Ч�������ˡ���
        //else Debug.Log("!");
    }

   
}

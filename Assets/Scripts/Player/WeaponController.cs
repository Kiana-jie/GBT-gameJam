using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public EnemyDetector detector;
    public Transform weaponTransform;
    public float stabDistance = 1f;
    public float stabDuration = 0.1f;
    public float attackCooldown = 1f;
    public float attackRange = 1.5f;
    public int damage = 10;
    private float attackTimer;

    void Update()
    {
        Aim();
        AutoAttack();
    }

    public void Aim()
    {
        if (detector.currentTarget != null)
        {
            Vector2 direction = detector.currentTarget.position - weaponTransform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            weaponTransform.rotation = Quaternion.Euler(0, 0, angle);
            
        }
    }

    public void AutoAttack()
    {
        attackTimer -= Time.deltaTime;

        if (detector.currentTarget != null && attackTimer <= 0f)
        {
            float distance = Vector2.Distance(transform.position, detector.currentTarget.position);

            if (distance <= attackRange)
            {
                StartCoroutine(StabRoutine());
                attackTimer = attackCooldown;
            }
        }
    }

    IEnumerator StabRoutine()
    {
        Vector3 originalPos = weaponTransform.localPosition;

        // 正确使用 Player 的朝向 scale.x
        

        // 当前朝向（weaponTransform.right 已经指向敌人）
        Vector3 direction = weaponTransform.right; // right 是局部 x轴，表示朝向

        

        Vector3 targetLocalPos = originalPos + direction * stabDistance;
        float timer = 0f;

        // 启用碰撞器
        Collider2D weaponCollider = weaponTransform.GetComponent<Collider2D>();
        if (weaponCollider != null) weaponCollider.enabled = true;


        // 戳出去
        while (timer < stabDuration)
        {
            weaponTransform.localPosition = Vector2.Lerp(originalPos, targetLocalPos, timer / stabDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        // 回来
        timer = 0f;
        if (weaponCollider != null) weaponCollider.enabled = false;
        while (timer < stabDuration)
        {
            weaponTransform.localPosition = Vector2.Lerp(targetLocalPos, originalPos, timer / stabDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        weaponTransform.localPosition = originalPos;

        // 禁用碰撞器
        
    }



}

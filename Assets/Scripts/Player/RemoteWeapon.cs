using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject boomBulletPrefab;
    public EnemyDetector detector;
    public Transform weaponTransform;
    private float attackTimer;
    public float attackCooldown = 1f;
    public float attackRange = 1.5f;
    public bool isImproved = false;
    private void Awake()
    {
        detector = GameObject.Find("Player2").GetComponent<EnemyDetector>();
    }
    public void Fire()
    {
        if (!isImproved)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().targetDir = detector.currentTarget.position - weaponTransform.position;
        }
        else
        {
            GameObject boomBullet = Instantiate(boomBulletPrefab,transform.position, Quaternion.identity);
            boomBullet.GetComponent<BoomBullet>().targetDir = detector.currentTarget.position - weaponTransform.position;
        }
        
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
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        Aim();
        AutoAttack();
    }

    public void AutoAttack()
    {
        attackTimer -= Time.deltaTime;

        if (detector.currentTarget != null && attackTimer <= 0f)
        {
            float distance = Vector2.Distance(transform.position, detector.currentTarget.position);

            if (distance <= attackRange)
            {
                Fire();
                AudioManager.Instance.Play("shoot", gameObject);
                attackTimer = attackCooldown;
            }
        }
    }
}


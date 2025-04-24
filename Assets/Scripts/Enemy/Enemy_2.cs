using UnityEngine;

public class Enemy_2 : Enemy
{
    public float wanderRadius = 5f;
    

    public GameObject energyBeanPrefab;
    
    public float lifeTime = 10f;
    private float lifeTimer;
    private SpriteRenderer spriteRenderer;
    public Color flashColor = Color.yellow;
    public float flashSpeed = 2f; // 闪光速度
    private Color originalColor;
    

    private void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        energyBeanPrefab = (GameObject)Resources.Load("Prefabs/energyBean");
        players = GameObject.FindGameObjectsWithTag("Player");
    }
    private void Start()
    {
        
        lifeTimer = lifeTime;
        

        target = GetRandomWanderTarget();
    }
    private void Update()
    {
        Flash();

        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0)
        {
            //Destroy(gameObject);
        }

        if (rb.velocity != Vector2.zero)
        {
            RecoverSpeed();

        }
    }
    private void FixedUpdate()
    {
        Move(target);

        // 如果到达目标点，生成新目标
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            target = GetRandomWanderTarget();
        }

    }

    // 禁用锁定玩家
    public new void SearchTarget() { }

    public new void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log($"{name} took {damage} damage. Remaining: {health}");
        if (health <= 0)
        {
            Die();
        }
    }

    // 死亡时掉落能量豆
    public new void Die()
    {
        Instantiate(energyBeanPrefab, transform.position, Quaternion.identity);
        Debug.Log(energyBeanPrefab.name);
        base.Die();
    }

    public override void HandleAttack() { }

    public override void Attack(GameObject target) { }

    private Vector2 GetRandomWanderTarget()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        return (Vector2)transform.position + randomDirection * wanderRadius;
    }


    private void Flash()
    {
        float lerp = (Mathf.Sin(Time.time * flashSpeed) + 1f) / 2f; // 0~1之间波动
        spriteRenderer.color = Color.Lerp(originalColor, flashColor, lerp);

    }
}

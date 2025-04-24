using UnityEngine;

public class Enemy_2 : Enemy
{
    public float wanderRadius = 5f;
    

    public GameObject energyBeanPrefab;
    
    public float lifeTime = 10f;
    private float lifeTimer;
    private SpriteRenderer spriteRenderer;
    public Color flashColor = Color.yellow;
    public float flashSpeed = 2f; // �����ٶ�
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

        // �������Ŀ��㣬������Ŀ��
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            target = GetRandomWanderTarget();
        }

    }

    // �����������
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

    // ����ʱ����������
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
        float lerp = (Mathf.Sin(Time.time * flashSpeed) + 1f) / 2f; // 0~1֮�䲨��
        spriteRenderer.color = Color.Lerp(originalColor, flashColor, lerp);

    }
}

using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP = 100f;
    public float attackRange = 2f;
    public float chaseRange = 30f;
    public float movespeed = 3f;
    private Rigidbody rb;
    private Player player;
    public bool isSubmerged = false;
    public GameObject EnemyProjectilePrefab;

    private void Awake()
    {
        // Initialization code here
        
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    private void Start()
    {
        // Initialization code here
    }
    private void FixedUpdate()
    {
        chasePlayer();
        //change
    }

   
    private void chasePlayer()
{
    Vector3 direction = player.transform.position - transform.position;

    float distanceToPlayer = direction.magnitude;

    if (distanceToPlayer <= chaseRange)
    {
        direction.Normalize();
        rb.linearVelocity = direction * movespeed;
    }
    else
    {
        rb.linearVelocity = Vector3.zero;
    }
}

    private void attackPlayer()
    {
        //  logic to attack the player when in range
        Vector3 direction = player.transform.position - transform.position;
        float distanceToPlayer = direction.magnitude;
        EnemyProjectile projectile = EnemyProjectilePrefab.GetComponent<EnemyProjectile>();


    }

    private void Update()
    {
        // Call chase and attack methods based on conditions
        attackPlayer();
    }
}

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
    public Transform spawnpoint;
    public int spawnlimit = 5;
    public float ATKCooldown = 10f;
    public float ATKTimer = 0f;
    

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
        direction.Normalize();
        float distanceToPlayer = direction.magnitude;//tels enemy where player is

    if (distanceToPlayer <= chaseRange)
    {
        direction.Normalize();
        rb.linearVelocity = direction * movespeed;//moves toward player
    }
    else
    {
        rb.linearVelocity = Vector3.zero;
    }
}

   private void attackPlayer()
    {
        Instantiate(EnemyProjectilePrefab, spawnpoint.position, spawnpoint.rotation);
    }

    private void Update()
    {
        ATKTimer += Time.deltaTime;

    if (ATKTimer >= ATKCooldown)
    {
        if (GameObject.FindGameObjectsWithTag("EnemyProjectile").Length < spawnlimit)
        {
            attackPlayer();
            ATKTimer = 0f;
        }
    }
    }
    
    }


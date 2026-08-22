using UnityEngine;
using UnityEngine.Rendering.Universal;


public class EnemyProjectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed = 10f;
    public float damage = 10f;
    public float lifetime = 5f;
    public Player player;

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        rb.linearVelocity = transform.forward * moveSpeed;
    }
    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }
    private bool isPlayerHit = false;
    private void OnTriggerEnter(Collider other)
    {
        if (isPlayerHit) return;
        {
            Player player = GetComponent<Player>();

            if (player != null)
            {
                isPlayerHit = true;

                player.HP -= damage;
            }
        }
        Debug.Log("isPlayer" + isPlayerHit);

    }
}

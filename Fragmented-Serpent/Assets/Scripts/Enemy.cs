using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP = 100f;

    public float attackRange = 3f;
    public float chaseRange = 30f;
    public float movespeed = 3f;

    private Rigidbody rb;
    private Player player;


    public GameObject EnemyProjectilePrefab;
    public Transform spawnpoint;

    public int spawnlimit = 5;
    public float ATKCooldown = 5f;
    public float ATKTimer = 0f;


   
}
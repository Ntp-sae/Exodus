using UnityEngine;
using UnityEngine.AI;  // For pathfinding

public class EnemyController : MonoBehaviour
{
    public Rigidbody rb;

    public bool isAlive;
    public bool disableEnemyMovement;
    public bool isGrounded;
    private float enemySpeed = 1.0f;
    private float maxVelocity;

    Animator animator;
    string currentState;
    const string ENEMY_IDLE = "Enemy_Idle";
    const string ENEMY_RUN = "Enemy_Run";
    const string ENEMY_SIDESTEP = "Enemy_Sidestep";
    const string ENEMY_BACKSTEP = "Enemy_Backstep";

    //const string ENEMY_JUMP = "Enemy_Jump"; If we can make an animation for it
    const string ENEMY_DIE = "Enemy_Die";

    //Attacks
    const string ENEMY_ATTACK_CLAW_LEFT = "Enemy_Attack_Claw_Left";
    const string ENEMY_ATTACK_CLAW_RIGHT = "Enemy_Attack_Claw_Right";
    const string ENEMY_ATTACK_CLAW_DOUBLE = "Enemy_Attack_Claw_Double";
    const string ENEMY_ATTACK_STINGER = "Enemy_Attack_Stringer";
    

    private void Awake()
    {
        disableEnemyMovement = true;
        enemySpeed = 1.0f;
        isGrounded = true;
        isAlive = true;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if(disableEnemyMovement)
        {

        }
    }

    //LOGIC
    

    //public float detectionRange = 10f;
    //public float attackRange = 2f;
    //public float patrolSpeed = 3.5f;
    //public float chaseSpeed = 6f;
    //public Transform player;
    //private NavMeshAgent navMeshAgent;
    //private Animator animator;

    //void Start()
    //{
    //    navMeshAgent = GetComponent<NavMeshAgent>();
    //    animator = GetComponent<Animator>();
    //    navMeshAgent.speed = patrolSpeed; 
    //}

    //void Update()
    //{
    //    Logic();
    //}

    //void Logic()
    //{
    //    float distanceToPlayer = Vector3.Distance(player.position, transform.position);

    //    if (distanceToPlayer <= attackRange)
    //    {
    //        AttackPlayer();
    //    }
    //    else if (distanceToPlayer <= detectionRange)
    //    {
    //        ChasePlayer();
    //    }
    //    else
    //    {
    //        Patrol();
    //    }
    //}

    //void Patrol()
    //{
    //    animator.SetBool("isWalking", true);
    //    navMeshAgent.isStopped = false;
    //    navMeshAgent.speed = patrolSpeed; 
    //}

    //void ChasePlayer()
    //{
    //    animator.SetBool("isWalking", true);
    //    navMeshAgent.isStopped = false;
    //    navMeshAgent.speed = chaseSpeed; 
    //    navMeshAgent.SetDestination(player.position);
    //}

    //void AttackPlayer()
    //{
    //    animator.SetBool("isWalking", false);
    //    navMeshAgent.isStopped = true;
    //    animator.SetTrigger("Attack");
    //}
}

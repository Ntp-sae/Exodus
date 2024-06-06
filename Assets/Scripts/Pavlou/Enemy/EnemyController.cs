using UnityEngine;
using UnityEngine.AI;  // For pathfinding

public class EnemyController : MonoBehaviour
{
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float patrolSpeed = 3.5f;
    public float chaseSpeed = 6f;
    public Transform player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.speed = patrolSpeed; 
    }

    void Update()
    {
        Logic();
    }

    void Logic()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        animator.SetBool("isWalking", true);
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = patrolSpeed; 
    }

    void ChasePlayer()
    {
        animator.SetBool("isWalking", true);
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = chaseSpeed; 
        navMeshAgent.SetDestination(player.position);
    }

    void AttackPlayer()
    {
        animator.SetBool("isWalking", false);
        navMeshAgent.isStopped = true;
        animator.SetTrigger("Attack");
    }
}

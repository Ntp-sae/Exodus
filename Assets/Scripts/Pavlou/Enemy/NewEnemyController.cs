using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NewEnemyController : MonoBehaviour
{
    #region Stats
    [SerializeField] float speed = 1f;
    [SerializeField] float chaseSpeed = 2f;
    [SerializeField] float rageSpeed = 5f;
    [SerializeField] float health = 1000f;
    [SerializeField] float MaxHealth = 1000f;
    [SerializeField] float AttackRange = 4f;
    private bool canAttack = true;
    [SerializeField] float AttackDamage;
    float distanceToPlayer;
    [SerializeField] bool chasePlayer;
    [SerializeField] Transform initialPosition;
    [SerializeField] bool returnToSpawn = false;
    private PlayerController player;
    private bool isAlive = true;
    [SerializeField] bool canDodge = true;
    [SerializeField] bool gotHit = false;
    //[SerializeField] Collider thisCollider;???
    [SerializeField] GameObject particleEffect;
    [SerializeField] Transform initialParticlePosition;
    #endregion
    //Test Section
    public float Testspeed;
    public bool PoisonBreath = false;
    public float timeElapsed;
    #region Timers
    [SerializeField] float attackDelay = 2f;
    [SerializeField] float currentAttackTimer;
    [SerializeField] float idleDelay = 2f;
    [SerializeField] float dodgeDelay = 2f;
    private float currentDodgeTimer;
    private float checkHealthTimer = 1f;
    private float timerThreshold = 2f;
    #endregion

    NavMeshAgent agent;
    Animator thisAnimator;
    AudioSource audioSource;
    private float UserVolumePreferred = 0.5f;
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack,
        Death,
        GetHit
    }
    public EnemyState currentState;
    // Start is called before the first frame update
    void Start()
    {
        ResetEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        //TEST SECTION
        Testspeed = agent.speed;
        //===================

        if (PoisonBreath)
        {
            SpecialAttack();
        }
        Logic();
        switch (currentState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Chase:
                Chase();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Death:
                Death();
                break;
            //case EnemyState.GetHit:
            //    GetHit();
            //    break;
            default:
                break;
        }

    }

    void Logic()
    {

        if (!isAlive) return;

        if (health <= 0)
        {
            isAlive = false;
            currentState = EnemyState.Death;
            return;
        }
        if (gotHit)
        {
            timerThreshold -= Time.deltaTime;
            if (timerThreshold <= 0)
            {
                gotHit = false;
                timerThreshold = 2f;
            }
        }
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        Dodge();
        ResetPosition();

        if (!canAttack)
        {
            currentAttackTimer += Time.deltaTime;
            if (currentAttackTimer >= attackDelay)
            {
                currentAttackTimer = 0;
                canAttack = true;
            }
        }
        if (chasePlayer)
        {

            currentState = EnemyState.Chase;
            if (distanceToPlayer <= AttackRange)
            {
                currentState = EnemyState.Idle;
                if (canAttack)
                {
                    currentState = EnemyState.Attack;
                }
            }

        }


    }

    void Idle()
    {
        agent.speed = 0;
        thisAnimator.SetFloat("Speed", agent.speed);
        agent.isStopped = true;
        agent.ResetPath();
    }

    void Dodge()
    {
        if (canDodge && distanceToPlayer>=AttackRange+1f)
        {
            if (gotHit)
            {
                agent.ResetPath();
                int direction = Random.Range(0, 2);
                if (direction == 0)
                {
                    thisAnimator.SetInteger("DodgePattern", direction);
                    thisAnimator.SetTrigger("Dodge");
                    transform.LookAt(player.transform.position);
                    //TODO TURN OFF COLLIDER WHILE DODGE ANIMATION IS PLAYING
                    //RAYCAST TO CHECK DODGE DIRECTION
                }
                else
                {
                    thisAnimator.SetInteger("DodgePattern", direction);
                    thisAnimator.SetTrigger("Dodge");
                    transform.LookAt(player.transform.position);
                }
                canDodge = false;
                currentState = EnemyState.Chase;
            }
        }
        else
        {
            currentDodgeTimer += Time.deltaTime;
            if (currentDodgeTimer >= dodgeDelay)
            {
                currentDodgeTimer = 0;
                canDodge = true;
            }
        }
    }
    void Chase()
    {
        agent.isStopped = false;
        agent.SetDestination(player.transform.position);
        agent.speed = speedCalculation();
        thisAnimator.SetFloat("Speed", agent.speed);
    }

    void Attack()
    {
        //Vector3 alignedPosition = player.transform.position;
        //alignedPosition.y = transform.position.y;

        transform.LookAt(player.transform);
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        thisAnimator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);

        int attackRandomizer = Random.Range(0, 4);
        Debug.Log(attackRandomizer);
        // Reset timer
        currentAttackTimer = 0;
        thisAnimator.SetInteger("AttackPattern", attackRandomizer);
        thisAnimator.SetTrigger("Attack");
        canAttack = false;
    }

    void Death()
    {
        thisAnimator.SetTrigger("Die");
        agent.speed = 0f;
        agent.isStopped = true;
        agent.ResetPath();
    }

    //TODO
    //void GetHit()
    //{

    //}

    void ResetEnemy()
    {
        health = MaxHealth;
        isAlive = true;
        thisAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = chaseSpeed;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = UserVolumePreferred;
        ResetBreathTime();
        //TODO PLAYERCONTROLLER TO SINGLETON
        player = FindObjectOfType<PlayerController>();
    }

    void ResetPosition()
    {
        if (!chasePlayer && returnToSpawn)
        {
            agent.SetDestination(initialPosition.transform.position);
            float distanceFromInitialPosition = Vector3.Distance(transform.position, initialPosition.position);
            if (distanceFromInitialPosition <= 1f)
            {
                returnToSpawn = false;
                return;
            }
        }
    }
    public void ChasePlayer()
    {
        chasePlayer = true;
    }
    void SetBoolReturnToSpawn()
    {
        //TODO
        //NavMeshObstacle Component on Doors to reset position to 
        //Enable-Disable Component with Script based on Door's State (opened/close)
        returnToSpawn = true;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        gotHit = true;
    }
    private float speedCalculation()
    {
        if (health <= MaxHealth / 2)
        {
            return rageSpeed;
        }
        else return speed;
    }
    //private bool CheckHealth(float tmpHealth)
    //{

    //}
    public void SpecialAttack()
    {
        particleEffect.transform.position = initialParticlePosition.transform.position;
        Vector3 direction = player.transform.position - initialParticlePosition.transform.position;
        particleEffect.transform.rotation = Quaternion.LookRotation(direction);
        particleEffect.SetActive(true);
        timeElapsed -= Time.deltaTime;

        if (timeElapsed <= 0f)
        {
            particleEffect.SetActive(false);
            ResetBreathTime();
            PoisonBreath = false;
        }
    }

    private void ResetBreathTime()
    {
        timeElapsed = particleEffect.GetComponent<ParticleSystem>().main.duration + 0.5f;
    }
}

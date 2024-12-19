using UnityEngine;
using UnityEngine.AI;
public enum EnemyState
{
    Patrol,
    Chase,
    Attack,

}

public class EnemyController : MonoBehaviour
{
    public const string PLAYER = "Player";
    private Transform target;
    private EnemyState enemyState;
    public EnemyState EnemyState { get; set; }
    private EnemyAnimator enemyAnim;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private float walkSpeed = 0.5f;
    [SerializeField] private float runSpeed = 4f;
    public float chaseDistance = 20f;
    [SerializeField] private float attackDistance = 2.2f;
    [SerializeField] private float chaseAfterAttackDistance = 2f;
    [SerializeField] private float patrolRadiusMin = 20f, patrolRadiusMax = 60f;
    [SerializeField] private float patrolForThisTime = 15f;
    [SerializeField] private float waitBeforeAttack = 2f;
    private float currentChaseDistance;
    private float patrolTimer;
    private float attackTimer;
    private void Awake()
    {
        enemyAnim = GetComponent<EnemyAnimator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(PLAYER).transform;
    }
    private void Start()
    {
        enemyState = EnemyState.Patrol;
        patrolTimer = patrolForThisTime;
        attackTimer = waitBeforeAttack;
        currentChaseDistance = chaseDistance;
    }
    private void Update()
    {
        if (enemyState == EnemyState.Patrol)
        {
            Patrol();
        }
        if (enemyState == EnemyState.Chase)
        {
            Chase();
        }
        if (enemyState == EnemyState.Attack)
        {
            Attack();
        }
    }
    private void Patrol()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = walkSpeed;
        patrolTimer += Time.deltaTime;
        if (patrolTimer > patrolForThisTime)
        {
            SetNewRandomDestination();
            patrolTimer = 0f;
        }
        if (navMeshAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnim.Walk(true);
        }
        else
        {
            enemyAnim.Walk(false);
        }
        if (Vector3.Distance(transform.position, target.position) <= chaseDistance)
        {
            enemyAnim.Walk(false);
            enemyState = EnemyState.Chase;
        }
    }
    private void Chase()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = runSpeed;
        navMeshAgent.SetDestination(target.position);
        if (navMeshAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnim.Run(true);
        }
        else
        {
            enemyAnim.Run(false);
        }
        if (Vector3.Distance(transform.position, target.position) <= attackDistance)
        {
            enemyAnim.Run(false);
            enemyAnim.Walk(false);
            enemyState = EnemyState.Attack;
            if (chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        }
        else if (Vector3.Distance(transform.position, target.position) > chaseDistance)
        {
            enemyAnim.Run(false);
            enemyState = EnemyState.Patrol;
            patrolTimer = patrolForThisTime;
            if (chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        }
    }
    private void Attack()
    {
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = true;
        attackTimer += Time.deltaTime;
        if (attackTimer > waitBeforeAttack)
        {
            enemyAnim.Attack();
            attackTimer = 0f;
        }
        if (Vector3.Distance(transform.position, target.position) > attackDistance + chaseAfterAttackDistance)
        {
            enemyState = EnemyState.Chase;
        }
    }
    private void SetNewRandomDestination()
    {
        float randomRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);
        Vector3 randomDirection = Random.insideUnitSphere * randomRadius;
        randomDirection += transform.position;

        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(randomDirection, out navMeshHit, randomRadius, -1);
        navMeshAgent.SetDestination(navMeshHit.position);
    }
    private void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }
    private void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }
}

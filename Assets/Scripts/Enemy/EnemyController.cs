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
    private EnemyAnimator enemyAnim;
    private NavMeshAgent navMeshAgent;
    public float walkSpeed = 0.5f;
    public float runSpeed = 4f;
    public float chaseDistance = 7f;
    public float attackDistance = 1.8f;
    public float chaseAfterAttackDistance = 2f;
    public float patrolRadiusMin = 20f, patrolRadiusMax = 60f;
    public float patrolForThisTime = 15f;
    public float waitBeforeAttack = 15f;
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
        //navMeshAgent.isStopped = false;
        navMeshAgent.speed = walkSpeed;
        patrolTimer += Time.deltaTime;
        if (patrolTimer < patrolForThisTime)
        {
            SetNewRandomDestination();
            patrolTimer = 0f;
        }
    }
    private void Chase()
    {
        navMeshAgent.speed = runSpeed;
    }
    private void Attack()
    {

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
}

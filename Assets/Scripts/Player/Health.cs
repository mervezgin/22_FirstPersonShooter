using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navMeshAgent;
    private EnemyController enemyController;
    [SerializeField] private float health = 100.0f;
    [SerializeField] private bool isPlayer, isBoar, isCannibal;
    private bool isDead;
    private void Awake()
    {
        if (isBoar || isCannibal)
        {
            enemyAnimator = GetComponent<EnemyAnimator>();
            enemyController = GetComponent<EnemyController>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        if (isPlayer)
        {

        }
    }
    public void ApplyDamage(float damage)
    {
        if (isDead) return;
        health -= damage;
        if (isPlayer)
        {

        }
        if (isBoar || isCannibal)
        {
            if (enemyController.EnemyState == EnemyState.Patrol)
            {
                enemyController.chaseDistance = 50.0f;
            }
        }
    }
}

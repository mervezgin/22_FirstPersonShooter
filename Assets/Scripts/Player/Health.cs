using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public const string ENEMY = "Enemy";
    public const string PLAYER = "Player";
    public const string FIRST_PERSON_SHOOTER = "FirstPersonShooter";
    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navMeshAgent;
    private EnemyController enemyController;
    private EnemyAudio enemyAudio;
    private PlayerStats playerStats;
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
            enemyAudio = GetComponentInChildren<EnemyAudio>();
        }
        if (isPlayer)
        {
            playerStats = GetComponent<PlayerStats>();
        }
    }
    public void ApplyDamage(float damage)
    {
        if (isDead) return;
        health -= damage;
        if (isPlayer)
        {
            playerStats.DisplayHealthStats(health);
        }
        if (isBoar || isCannibal)
        {
            if (enemyController.EnemyState == EnemyState.Patrol)
            {
                enemyController.chaseDistance = 50.0f;
            }
        }
        if (health <= 0.0f)
        {
            Debug.Log("HEALTH = ZERO");
            PlayerDied();
            isDead = true;
        }
    }
    private void PlayerDied()
    {
        if (isCannibal)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            gameObject.AddComponent<Rigidbody>().AddTorque(-transform.forward * 50.0f);
            enemyController.enabled = false;
            navMeshAgent.enabled = false;
            enemyAnimator.enabled = false;
            StartCoroutine(DeadSound());
            EnemyManager.instance.EnemyDied(true);
        }
        if (isBoar)
        {
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.isStopped = true;
            enemyController.enabled = false;
            enemyAnimator.Dead();
            StartCoroutine(DeadSound());
            EnemyManager.instance.EnemyDied(false);
        }
        if (isPlayer)
        {
            Debug.Log("ÖLÜYORUM");

            EnemyManager.instance.StopSpawning();

            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);

            Debug.Log("SİLAHIMI KAYBETTİM");

            GameObject[] enemies = GameObject.FindGameObjectsWithTag(ENEMY);
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponentInChildren<EnemyController>().enabled = false;
                Debug.Log("YAM YAM HAREKETİNİ DURDURDU");
            }
        }
        if (tag == PLAYER)
        {
            Debug.Log("OYUN YENİDEN BAŞLIYOR");
            Invoke("RestartGame", 3.0f);
        }
        else
        {
            Invoke("TurnOffGameObject", 3.0f);
        }
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(FIRST_PERSON_SHOOTER);
    }
    private void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.PlayDeadSound();
    }
}

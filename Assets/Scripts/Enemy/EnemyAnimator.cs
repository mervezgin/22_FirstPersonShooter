using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public const string ENEMYWALK = "Walk";
    public const string ENEMYRUN = "Run";
    public const string ENEMYATTACK = "Attack";
    public const string ENEMYDEAD = "Dead";
    private Animator enemyAnimator;
    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
    }
    public void Walk(bool walk)
    {
        enemyAnimator.SetBool(ENEMYWALK, walk);
    }
    public void Run(bool run)
    {
        enemyAnimator.SetBool(ENEMYRUN, run);
    }
    public void Attack()
    {
        enemyAnimator.SetTrigger(ENEMYATTACK);
    }
    public void Dead()
    {
        enemyAnimator.SetTrigger(ENEMYDEAD);
    }
}

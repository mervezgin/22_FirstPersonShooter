using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [SerializeField] private GameObject boarPrefab, cannibalPrefab;
    [SerializeField] private Transform[] cannibalSpawnPoints, boarSpawnPoints;
    [SerializeField] private float waitBeforeSpawnEnemiesTime = 10.0f;
    [SerializeField] private int cannibalEnemyCount, boarEnemyCount;
    private int initialCannibalCount, initialBoarCount;

    private void Awake()
    {
        MakeInstance();
    }
    private void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}

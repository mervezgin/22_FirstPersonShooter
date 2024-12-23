using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
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

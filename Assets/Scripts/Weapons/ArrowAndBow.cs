using UnityEngine;

public class ArrowAndBow : MonoBehaviour
{
    public const string ENEMY = "Enemy";
    private Rigidbody rb;
    private float speed = 30f;
    private float deactivateTimer = 3f;
    private float damage = 50f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }
    private void Start()
    {
        Invoke("DeactivateGameObject", deactivateTimer);
    }
    private void DeactivateGameObject()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
    public void Launch(Camera mainCamera)
    {
        rb.linearVelocity = mainCamera.transform.forward * speed;
        transform.LookAt(transform.position + rb.linearVelocity);
    }
    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == ENEMY)
        {
            target.GetComponentInChildren<Health>().ApplyDamage(damage);
            gameObject.SetActive(false);
        }
    }
}

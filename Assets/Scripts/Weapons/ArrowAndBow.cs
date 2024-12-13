using UnityEngine;

public class ArrowAndBow : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 30f;
    private float deactivateTimer = 3f;
    //private float damage = 15f;
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

    }
}

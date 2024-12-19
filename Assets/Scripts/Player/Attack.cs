using UnityEngine;

public class Attack : MonoBehaviour
{
    //[SerializeField] private float damage = 2.0f;
    [SerializeField] private float radius = 1.0f;
    [SerializeField] private LayerMask layerMask;
    private void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);
        if (hits.Length > 0)
        {
            Debug.Log("WE TOUCHED " + hits[0].gameObject.tag);
            gameObject.SetActive(false);
        }
    }
}

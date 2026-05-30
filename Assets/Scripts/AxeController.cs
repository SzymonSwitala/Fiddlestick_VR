using UnityEngine;

public class AxeController : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int damage = 1;



    private void OnTriggerEnter(Collider other)
    {
        TotemDestruction totem = other.GetComponentInParent<TotemDestruction>();

        if (totem == null)
            return;

        Vector3 hitPoint = other.ClosestPoint(transform.position);
        totem.TakeDamage(damage, hitPoint);
    }
}
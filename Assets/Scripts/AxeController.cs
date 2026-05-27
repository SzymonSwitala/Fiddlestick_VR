using UnityEngine;

public class AxeController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;

    [Header("Damage")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float minSwingVelocity = 1.5f;

    [Header("Hit Settings")]
    [SerializeField] private float hitCooldown = 0.15f;

    private float lastHitTime;
    private bool isSwinging;

    public float CurrentVelocity { get; private set; }

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CurrentVelocity = rb.linearVelocity.magnitude;

        isSwinging = CurrentVelocity >= minSwingVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isSwinging)
            return;

        if (Time.time - lastHitTime < hitCooldown)
            return;

        TotemDestruction totem = other.GetComponent<TotemDestruction>();

        if (totem == null)
            return;

        lastHitTime = Time.time;

        Vector3 hitPoint = other.ClosestPoint(transform.position);

        totem.TakeDamage(damage, hitPoint);
    }
}
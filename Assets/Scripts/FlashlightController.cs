using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Light flashlightBeam;

    [Header("Break Settings")]
    [SerializeField] private float breakChancePerSecond = 0.03f;
    [SerializeField] private float shakeThreshold = 2.5f;

    [Header("Detection Settings")]
    [SerializeField] private float range = 15f;
    [SerializeField] private float sphereRadius = 1.5f;
    [SerializeField] private LayerMask enemyLayer;

    private bool isBroken = false;

    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (isBroken)
        {
            CheckForShake();
            return;
        }

        SimulateRandomFailure();
        CheckForEnemy();
    }

    private void SimulateRandomFailure()
    {
        if (Random.value < breakChancePerSecond * Time.deltaTime)
        {
            BreakFlashlight();
        }
    }

    private void BreakFlashlight()
    {
        isBroken = true;

        if (flashlightBeam != null)
        {
            flashlightBeam.enabled = false;
        }

        Debug.Log("Flashlight broken");
    }

    private void FixFlashlight()
    {
        if (!isBroken)
            return;

        isBroken = false;

        if (flashlightBeam != null)
        {
            flashlightBeam.enabled = true;
        }

        Debug.Log("Flashlight fixed");
    }

    private void CheckForShake()
    {
        Vector3 currentPosition = transform.position;

        Vector3 velocity =
            (currentPosition - lastPosition) / Time.deltaTime;

        lastPosition = currentPosition;

        if (velocity.magnitude >= shakeThreshold)
        {
            FixFlashlight();
        }
    }

    private void CheckForEnemy()
    {
        Vector3 origin = transform.position + transform.forward * 0.2f;

        if (Physics.SphereCast(
                origin,
                sphereRadius,
                transform.forward,
                out RaycastHit hit,
                range,
                enemyLayer))
        {
            EnemyController enemy =
                hit.collider.GetComponentInParent<EnemyController>();

            if (enemy != null)
            {
                enemy.AddExposure(Time.deltaTime);

                Debug.Log("Enemy detected");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position + transform.forward * 0.2f;
        Vector3 end = origin + transform.forward * range;

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(origin, end);

        Gizmos.DrawWireSphere(origin, sphereRadius);
        Gizmos.DrawWireSphere(end, sphereRadius);
    }
}
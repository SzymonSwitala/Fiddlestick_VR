using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private Light flashlightBeam;
    [SerializeField] private float shakeThreshold = 2.0f;
    [Header("Detection Settings")]
    [SerializeField] private float range = 15f;
    [SerializeField] private float sphereRadius = 2f;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] bool isBroken = false;
    private Vector3 lastPosition;

    void Update()
    {
        if (isBroken)
        {
            CheckForShake();
        }
        else
        {
            SimulateRandomFailure();
            CheckForEnemy();
        }
    }

    void SimulateRandomFailure()
    {
        if (Random.value < 0.001f)
        {
            BreakFlashlight();
        }
    }

    void BreakFlashlight()
    {
        isBroken = true;
        flashlightBeam.enabled = false;
    }

    void CheckForShake()
    {
        Vector3 deviceVelocity = (transform.localPosition - lastPosition) / Time.deltaTime;
        lastPosition = transform.localPosition;

        if (deviceVelocity.magnitude > shakeThreshold)
        {
            FixFlashlight();
        }
    }

    void FixFlashlight()
    {
        isBroken = false;
        flashlightBeam.enabled = true;
    }
    void CheckForEnemy()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, sphereRadius, transform.forward, out hit, range, enemyLayer))
        {
            EnemyController enemy = hit.collider.GetComponentInParent<EnemyController>();
            if (enemy != null)
            {
                Debug.Log("Enemy detected in beam!");
                enemy.AddExposure();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * range);
        Gizmos.DrawWireSphere(transform.position + transform.forward * range, sphereRadius);
    }
}
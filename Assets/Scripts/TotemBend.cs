using UnityEngine;

public class TotemBend : MonoBehaviour
{
    public float stiffness = 25f;  
    public float damping = 6f;  
    public float impactStrength = 0.3f;

    private Vector3 velocity;
    private Vector3 targetOffset;

    private Vector3 restPosition;

    void Start()
    {
        restPosition = transform.localPosition;
    }

    void Update()
    {
        Vector3 force = -stiffness * targetOffset - damping * velocity;

        velocity += force * Time.deltaTime;
        targetOffset += velocity * Time.deltaTime;

        transform.localPosition = restPosition + targetOffset;
    }

    public void ApplyHit(Vector3 hitPoint, Vector3 hitDirection)
    {
        Vector3 dir = (transform.position - hitPoint).normalized;

        velocity += dir * impactStrength;

        targetOffset += dir * impactStrength;
    }
}
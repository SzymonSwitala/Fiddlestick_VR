using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ReturnToStartOnRelease : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    [SerializeField] private float returnSpeed = 5f;
    private bool isReturning = false;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        startPosition = transform.position;
        startRotation = transform.rotation;

        grabInteractable.selectExited.AddListener(OnRelease);
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    void OnDestroy()
    {
        grabInteractable.selectExited.RemoveListener(OnRelease);
        grabInteractable.selectEntered.RemoveListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        isReturning = false;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isReturning = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
    }

    void Update()
    {
        if (!isReturning) return;

        transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * returnSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, Time.deltaTime * returnSpeed);

        if (Vector3.Distance(transform.position, startPosition) < 0.01f)
        {
            transform.position = startPosition;
            transform.rotation = startRotation;

            rb.useGravity = true;
            isReturning = false;
        }
    }
}
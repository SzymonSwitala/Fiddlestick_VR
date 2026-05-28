using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.Events;

public class ReturnToStartOnRelease : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    [Header("Return Settings")]
    [SerializeField] private float returnSpeed = 5f;
    private bool isReturning = false;

    [Header("Events")]
    public UnityEvent onGrab;
    public UnityEvent onRelease;
    public UnityEvent onReturned;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        startPosition = transform.position;
        startRotation = transform.rotation;

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        isReturning = false;

        rb.useGravity = true;

        onGrab?.Invoke();
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isReturning = true;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;

        onRelease?.Invoke();
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

            onReturned?.Invoke();
        }
    }
}
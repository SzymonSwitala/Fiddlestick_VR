using UnityEngine;
using UnityEngine.XR; 
using System.Collections.Generic;

public class AxeControllerVR : MonoBehaviour
{
    [Header("Combat Stats")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float hitCooldown = 0.3f;

    [SerializeField] private float minHitVelocity = 2.0f;

    [Header("VR Haptics")]
    [SerializeField] private bool isLeftHand = false;
    [Range(0f, 1f)][SerializeField] private float hapticAmplitude = 0.5f;
    [SerializeField] private float hapticDuration = 0.15f;             

    private float lastHitTime = 0f;
    private Vector3 previousPosition;
    private float currentVelocity;

    private void Start()
    {
        previousPosition = transform.position;
    }

    private void FixedUpdate()
    {
        currentVelocity = (transform.position - previousPosition).magnitude / Time.fixedDeltaTime;
        previousPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < lastHitTime + hitCooldown)
            return;

        if (currentVelocity < minHitVelocity)
            return;

        TotemDestruction totem = other.GetComponentInParent<TotemDestruction>();

        if (totem == null)
            return;

        Vector3 hitPoint = other.ClosestPoint(transform.position);
        totem.TakeDamage(damage, hitPoint);
        lastHitTime = Time.time;

        TriggerHapticFeedback();
    }

    private void TriggerHapticFeedback()
    {
        InputDeviceCharacteristics characteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;
        characteristics |= isLeftHand ? InputDeviceCharacteristics.Left : InputDeviceCharacteristics.Right;

        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(characteristics, devices);

        if (devices.Count > 0)
        {
            devices[0].SendHapticImpulse(0, hapticAmplitude, hapticDuration);
        }
    }
}
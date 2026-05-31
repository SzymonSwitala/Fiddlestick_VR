using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR; // Wymagane do obsługi przycisków VR
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    [Header("Game Events")]
    public UnityEvent onGameOver;
    public UnityEvent onVictory;

    // Zmienna zapamiętująca stan przycisku, żeby resetować mapę tylko w momencie wciśnięcia (a nie przytrzymania)
    private bool wasPrimaryButtonPressed = false;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Update()
    {
   

        CheckVRInput();
    }

    private void CheckVRInput()
    {
        InputDeviceCharacteristics characteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        List<InputDevice> rightHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(characteristics, rightHandDevices);

        if (rightHandDevices.Count > 0)
        {
            InputDevice rightController = rightHandDevices[0];

            if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed))
            {
                if (isPressed && !wasPrimaryButtonPressed)
                {
                    ResetMap();
                }

                wasPrimaryButtonPressed = isPressed;
            }
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over! Gracz przegrał.");
        onGameOver?.Invoke();
    }

    public void EnemyDefeated()
    {
        Debug.Log("Enemy defeated! Gracz pokonał wroga.");
        onVictory?.Invoke();
    }

    public void ResetMap()
    {
        Debug.Log("Przeładowywanie mapy...");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
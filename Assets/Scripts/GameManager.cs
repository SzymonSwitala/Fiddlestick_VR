using UnityEngine;
using UnityEngine.Events; // Wymagane do używania UnityEvent

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
}
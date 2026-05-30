using UnityEngine;

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

    [SerializeField] private GameObject gameoverUI;
    [SerializeField] private GameObject victoryUI;

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
        if (gameoverUI != null)
        {
            Debug.Log("Game Over! Gracz przegrał.");
            gameoverUI.SetActive(true);
        }
    }

    public void EnemyDefeated()
    {
        Debug.Log("Enemy defeated! Gracz pokonał wroga."); 
        if (victoryUI != null)
        {
            victoryUI.SetActive(true);
        }
    }
}
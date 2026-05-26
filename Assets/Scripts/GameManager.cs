using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance;

    [SerializeField] private GameObject gameoverUI;

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
        gameoverUI.SetActive(true);
    }

    public void EnemyDefeated()
    {
        Debug.Log("Enemy defeated! Gracz zniknął wroga.");
    }

}
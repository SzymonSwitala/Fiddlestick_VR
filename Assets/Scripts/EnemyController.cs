using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float maxExposure = 2.0f;
    [SerializeField] private EnemySpawner spawner;

    [Header("Game Over Settings")]
    [SerializeField] private float timeToDefeatEnemy = 5.0f;

    private float currentExposure = 0f;
    private float defeatTimer = 0;

    private void OnEnable()
    {
        currentExposure = 0f;
        defeatTimer = timeToDefeatEnemy;
    }

    private void Update()
    {
        defeatTimer -= Time.deltaTime;

        if (defeatTimer <= 0f)
        {
            GameOver();
        }
    }
    public void AddExposure()
    {
        currentExposure += Time.deltaTime;

        if (currentExposure >= maxExposure)
        {
            Disappear();
            spawner.Respawn();
        }
    }
    public void Disappear()
    {
        gameObject.SetActive(false);
    }
    public void Respawn()
    {
        gameObject.SetActive(true);
    }
    private void GameOver()
    {
        Debug.Log("Game Over! Gracz nie zdążył zniknąć wroga.");
    }
}

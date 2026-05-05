using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float maxExposure = 2.0f;
    [SerializeField] private EnemySpawner spawner;
    private float currentExposure = 0f;
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
    public void ResetEnemy()
    {
        currentExposure = 0f;
        gameObject.SetActive(true);
    }
}

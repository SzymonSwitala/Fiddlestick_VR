using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> spawnPoints;
    [SerializeField] private GameObject enemyInstance;
    private EnemyController enemyController;

    [SerializeField] private float minRespawnTime = 2f;
    [SerializeField] private float maxRespawnTime = 5f;


    private void Start()
    {
        enemyController=enemyInstance.GetComponent<EnemyController>();
        Respawn();
    }
    public void Respawn()
    {
        StartCoroutine(RespawnRoutine());
    }
    private IEnumerator RespawnRoutine()
    {
        float waitTime = Random.Range(minRespawnTime, maxRespawnTime);
        yield return new WaitForSeconds(waitTime);

        SpawnPoint spawnPoint=GetRandomSpawnPoint();
        spawnPoint.Activate();

        Transform nextPoint = spawnPoint.transform; 

        enemyInstance.transform.position = nextPoint.position;
        enemyInstance.transform.rotation = nextPoint.rotation;

        enemyController.Respawn();

    }
    private SpawnPoint GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }

}

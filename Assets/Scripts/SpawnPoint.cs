using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private AudioSource spawnSoundSource;
    public int poseIndex;  

    private void Start()
    {
        spawnSoundSource = gameObject.GetComponent<AudioSource>();
    }
    public void Activate()
    {
        Debug.Log($"SpawnPoint {poseIndex} activated.");

        spawnSoundSource.Play();
       
    }
}

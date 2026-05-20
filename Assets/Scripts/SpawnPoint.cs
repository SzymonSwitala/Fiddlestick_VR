using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private AudioSource spawnSoundSource;
    private void Start()
    {
        spawnSoundSource = gameObject.GetComponent<AudioSource>();
    }
    public void Activate()
    {
       
          spawnSoundSource.Play();
       
    }
}

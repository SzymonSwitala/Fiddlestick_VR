using UnityEngine;

public class TotemDestruction : MonoBehaviour
{
    public int health = 50;
    public GameObject fracturedVersion;
    public GameObject woodSplinters;
    public TotemBend bend;
    public AudioSource hitSound;

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        hitSound.Play();

        health -= amount;
        Instantiate(woodSplinters, hitPoint, Quaternion.identity);
        Debug.Log($"Totem took {amount} damage at {hitPoint}. Remaining health: {health}");

        if (health <= 0)
        {
            DestroyTotem();
        }
    }

    void DestroyTotem()
    {
        fracturedVersion.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
using UnityEngine;

public class TotemDestruction : MonoBehaviour
{
    public int health = 50;
    public GameObject fracturedVersion;
    public GameObject woodSplinters;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Totem hit by: " + collision.gameObject.name + " with velocity: " + collision.relativeVelocity.magnitude);
        if (collision.gameObject.CompareTag("Axe") && collision.relativeVelocity.magnitude > 2.0f)
        {
            Debug.Log("Totem takes damage from axe hit!");
            TakeDamage(1, collision.contacts[0].point);
        }
    }

    void TakeDamage(int amount, Vector3 hitPoint)
    {
        health -= amount;
        Instantiate(woodSplinters, hitPoint, Quaternion.identity);

//Trigger Haptic Feedback

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
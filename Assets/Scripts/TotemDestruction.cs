using UnityEngine;

public class TotemDestruction : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Destruction Stages")]
    public GameObject[] damageStages;
    private int currentStageIndex = 0;

    [Header("References")]
    public GameObject woodSplinters;
    public TotemBend bend;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] hitSounds;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateDamageStage();
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (audioSource != null && hitSounds != null && hitSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, hitSounds.Length);
            audioSource.PlayOneShot(hitSounds[randomIndex]);
        }

        currentHealth -= amount;

        if (woodSplinters != null)
        {
            Instantiate(woodSplinters, hitPoint, Quaternion.identity);
        }

        Debug.Log($"Totem took {amount} damage at {hitPoint}. Remaining health: {currentHealth}");

        if (currentHealth <= 0)
        {
            DestroyTotem();
        }
        else
        {
            CheckDamageStage();
        }
    }

    private void CheckDamageStage()
    {
        if (damageStages == null || damageStages.Length == 0) return;

        float healthPercent = (float)currentHealth / maxHealth;

        int expectedStage = Mathf.FloorToInt((1f - healthPercent) * damageStages.Length);

        expectedStage = Mathf.Clamp(expectedStage, 0, damageStages.Length - 1);

        if (expectedStage != currentStageIndex)
        {
            currentStageIndex = expectedStage;
            UpdateDamageStage();

            // Opcjonalnie: Możesz tu dodać np. efekt cząsteczkowy "odpadającego kawałka" 
            // przy przejściu do kolejnego etapu.
        }
    }

    private void UpdateDamageStage()
    {
        for (int i = 0; i < damageStages.Length; i++)
        {
            if (damageStages[i] != null)
            {
                damageStages[i].SetActive(i == currentStageIndex);
            }
        }
    }

    private void DestroyTotem()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.EnemyDefeated();
        }

        // Destroy(gameObject);
    }
}
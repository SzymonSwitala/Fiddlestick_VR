using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform player;

    [SerializeField] private float maxExposure = 2.0f;
    [SerializeField] private EnemySpawner spawner;

    [Header("Game Over Settings")]
    [SerializeField] private float timeToDefeatEnemy = 5.0f;

    [Header("Chase Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackDistance = 1.5f;

    [Header("Attack Settings")]
    [SerializeField] private float attackDelay = 0.6f;

    private float currentExposure = 0f;
    private float defeatTimer = 0f;

    private bool isChasing = false;
    private bool isAttacking = false;

    private void OnEnable()
    {
        currentExposure = 0f;
        defeatTimer = timeToDefeatEnemy;

        isChasing = false;
        isAttacking = false;

        animator.SetBool("IsRunning", false);
    }

    private void Update()
    {
        if (isAttacking) return;

        if (!isChasing)
        {
            defeatTimer -= Time.deltaTime;

            if (defeatTimer <= 0f)
            {
                StartChasing();
            }
        }
        else
        {
            ChasePlayer();
        }
    }

    private void StartChasing()
    {
        isChasing = true;
        animator.SetBool("IsRunning", true);
    }

    private void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180, 0);
        }

        Vector3 enemyPos = transform.position;
        Vector3 playerPos = player.position;

        enemyPos.y = 0f;
        playerPos.y = 0f;

        float distance = Vector3.Distance(enemyPos, playerPos);

        if (distance <= attackDistance)
        {
            StartAttack();
            return;
        }

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void StartAttack()
    {
        isAttacking = true;
        animator.SetBool("IsRunning", false);
        animator.SetTrigger("JumpAttack");

        Invoke(nameof(GameOver), attackDelay);
    }

    public void AddExposure(float amount)
    {
        currentExposure += amount;

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

    public void Respawn(int poseIndex)
    {
        gameObject.SetActive(true);

        animator.SetInteger("PoseIndex", poseIndex);
        animator.SetBool("IsRunning", false);
    }

    private void GameOver()
    {
        GameManager.Instance.GameOver();
    }
}
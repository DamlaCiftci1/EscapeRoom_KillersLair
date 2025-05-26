using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPoint = 0;

    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;

    public float detectionRange = 6f;
    public float viewAngle = 90f;
    public float verticalTolerance = 2f;
    public GameObject gameOverPanel;

    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPoint].position);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float heightDifference = Mathf.Abs(transform.position.y - player.position.y);

        // Görüþ açýsý kontrolü
        Vector3 rayStartPos = transform.position + Vector3.up * 0.9f; // Daha alçaktan baþlat
        Vector3 directionToPlayer = (player.position + Vector3.up * 0.3f - rayStartPos).normalized;

        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        // Ray ile oyuncuyu görebiliyor mu kontrol et
        bool isPlayerVisible = distanceToPlayer < detectionRange &&
                               angleToPlayer < viewAngle / 2f &&
                               heightDifference < verticalTolerance &&
                               Physics.Raycast(rayStartPos, directionToPlayer, out RaycastHit hit, detectionRange) &&
                               hit.collider.CompareTag("Player");

        // Debug çizgisi (scene'de görmek istersen)
        Debug.DrawRay(rayStartPos, directionToPlayer * detectionRange, Color.red);

        if (isPlayerVisible)
        {
            isChasing = true;
            agent.SetDestination(player.position);

            if (distanceToPlayer < 0.4f)
            {
                HandleGameOver();
            }
        }
        else
        {
            if (isChasing)
            {
                isChasing = false;
                agent.SetDestination(patrolPoints[currentPoint].position);
            }

            Patrol();
        }

        // Animasyon kontrolü
        if (animator != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    void Patrol()
    {
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPoint].position);
        }
    }

    void HandleGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        agent.isStopped = true;
        Time.timeScale = 0f;
    }
}

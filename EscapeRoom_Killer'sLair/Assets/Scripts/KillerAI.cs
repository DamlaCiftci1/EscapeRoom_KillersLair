using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPoint = 0;

    private NavMeshAgent agent;
    private Transform player;

    public float detectionRange = 6f; // Görüþ menzili
    public float viewAngle = 90f;     // Toplam görüþ açýsý
    public float verticalTolerance = 2f; // Kat farký toleransý
    public GameObject gameOverPanel;     // Game Over paneli

    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        float heightDifference = Mathf.Abs(transform.position.y - player.position.y);

        bool isPlayerVisible = distanceToPlayer < detectionRange &&
                               angleToPlayer < viewAngle / 2f &&
                               heightDifference < verticalTolerance;

        if (isPlayerVisible)
        {
            isChasing = true;
            agent.SetDestination(player.position);

            // Eðer oyuncuya çok yakýnsa -> Game Over
            if (distanceToPlayer < 1.2f)
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
        Time.timeScale = 0f; // Oyunu durdur
    }
}

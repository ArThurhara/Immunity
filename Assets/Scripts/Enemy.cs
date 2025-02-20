using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum Phase
    {
        Patrol,
        Chase,
        Return
    }

    [Header("References")]
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private LayerMask obstacleLayer;

    [Header("Patrol Settings")]
    [SerializeField] private int numberOfPatrolPoints = 4;
    [SerializeField] private float patrolRadius = 3f;
    [SerializeField] private float pathResetThreshold = 5f;
    [SerializeField] private float stoppingDistance = 0.1f;
    [SerializeField] private float waitTimeAtPatrolPoint = 1f;

    [Header("Chase Settings")]
    [SerializeField] private float predictionTime = 0.5f;
    [SerializeField] private float chaseSpeedMultiplier = 0.2f;
    [SerializeField] private float returnSpeedMultiplier = 0.8f;

    [Header("Debug")]
    [SerializeField] private bool showDebugInfo = true;

    private Transform player;
    private List<Vector2> patrolPoints = new List<Vector2>();
    private int currentPatrolIndex = 0;
    private Phase currentPhase;
    private Vector2 targetPosition;
    private Vector2 initialPosition;
    private Vector2 lastKnownPlayerPosition;
    private float currentSpeed;
    private Vector2 velocity = Vector2.zero;
    private bool isWaitingAtPoint = false;

    private float health;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject DNA;
    private void Start()
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Player not found");
        }

        initialPosition = transform.position;
        GeneratePatrolPoints();
        currentSpeed = enemyStats.speed;
        health = enemyStats.health;
        currentPhase = Phase.Patrol;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameObject ex = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(ex, 0.2f);
            float drop = Random.Range(0, 100);
            if (drop <= enemyStats.dropProbability) {
                Instantiate(DNA, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (player == null) return;

        UpdatePhase();
        MoveEnemy();

        if (showDebugInfo)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            Debug.Log($"Distance to player: {distanceToPlayer}, Chase range: {enemyStats.chaseRange}, Current phase: {currentPhase}");
        }
    }

    private void UpdatePhase()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= enemyStats.chaseRange)
        {
            if (currentPhase != Phase.Chase)
            {
                Debug.Log("Entering chase phase!");
                currentPhase = Phase.Chase;
                currentSpeed = enemyStats.speed * chaseSpeedMultiplier;
                isWaitingAtPoint = false;
            }
            lastKnownPlayerPosition = player.position;
        }
        else if (currentPhase == Phase.Chase)
        {
            currentPhase = Phase.Return;
            currentSpeed = enemyStats.speed * returnSpeedMultiplier;
        }

        if (currentPhase == Phase.Return)
        {
            float distanceToPatrol = Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex]);
            if (distanceToPatrol < pathResetThreshold)
            {
                currentPhase = Phase.Patrol;
                currentSpeed = enemyStats.speed;
            }
        }
    }

    private void MoveEnemy()
    {
        if (isWaitingAtPoint) return;

        Vector2 desiredDirection = Vector2.zero;

        switch (currentPhase)
        {
            case Phase.Patrol:
                desiredDirection = Patrol();
                break;
            case Phase.Chase:
                desiredDirection = Pursue();
                break;
            case Phase.Return:
                desiredDirection = ReturnToPatrol();
                break;
        }

        // desiredDirection = AvoidObstacles(desiredDirection);

        float lerpSpeed = (currentPhase == Phase.Chase) ? 10f : 5f;
        velocity = Vector2.Lerp(velocity, desiredDirection * currentSpeed, Time.deltaTime * lerpSpeed);
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    private Vector2 Patrol()
    {
        if (patrolPoints.Count == 0) return Vector2.zero;

        targetPosition = patrolPoints[currentPatrolIndex];
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        if (Vector2.Distance(transform.position, targetPosition) < stoppingDistance && !isWaitingAtPoint)
        {
            StartCoroutine(WaitAtPatrolPoint());
        }

        return direction;
    }

    private Vector2 Pursue()
    {
        Vector2 directionToPlayer = ((Vector2)player.position - (Vector2)transform.position).normalized;
        
        if (player.GetComponent<Rigidbody2D>() != null)
        {
            Vector2 predictedPosition = (Vector2)player.position + 
                (Vector2)player.GetComponent<Rigidbody2D>().velocity * predictionTime;
            directionToPlayer = (predictedPosition - (Vector2)transform.position).normalized;
        }

        return directionToPlayer;
    }

    private Vector2 ReturnToPatrol()
    {
        return (patrolPoints[currentPatrolIndex] - (Vector2)transform.position).normalized;
    }

    private Vector2 AvoidObstacles(Vector2 direction)
    {
        float rayDistance = enemyStats.fieldOfView;
        Vector2 avoidanceDirection = direction;

        for (int i = -2; i <= 2; i++)
        {
            float angle = i * 30f;
            Vector2 rayDirection = Quaternion.Euler(0, 0, angle) * direction;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, rayDistance, obstacleLayer);

            if (hit.collider != null && hit.collider.gameObject.tag != "Player")
            {
                float weight = 1.0f - (hit.distance / rayDistance);
                avoidanceDirection += (Vector2)hit.normal * weight;
            }
        }

        return avoidanceDirection.normalized;
    }

    private void UpdateFacing()
    {
        if (velocity.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private IEnumerator WaitAtPatrolPoint()
    {
        isWaitingAtPoint = true;
        velocity = Vector2.zero;
        yield return new WaitForSeconds(waitTimeAtPatrolPoint);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        isWaitingAtPoint = false;
    }

    private void GeneratePatrolPoints()
    {
        patrolPoints.Clear();
        for (int i = 0; i < numberOfPatrolPoints; i++)
        {
            float angle = i * (360f / numberOfPatrolPoints);
            Vector2 point = (Vector2)transform.position + new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            ) * patrolRadius;

            point += Random.insideUnitCircle * (patrolRadius * 0.2f);
            patrolPoints.Add(point);
        }
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.blue;
    //     foreach (Vector2 point in patrolPoints)
    //     {
    //         Gizmos.DrawSphere(point, 0.1f);
    //     }

    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position, enemyStats.chaseRange);

    //     if (player != null)
    //     {
    //         Gizmos.color = Color.yellow;
    //         Gizmos.DrawLine(transform.position, player.position);

    //         float distance = Vector2.Distance(transform.position, player.position);
    //         Debug.DrawRay(transform.position, Vector2.up * 2,
    //             distance <= enemyStats.chaseRange ? Color.green : Color.red);
    //     }

    //     if (Application.isPlaying)
    //     {
    //         Gizmos.color = Color.white;
    //         Vector3 textPosition = transform.position + Vector3.up * 2.5f;
    //         UnityEditor.Handles.Label(textPosition, $"Phase: {currentPhase}");
    //     }
    // }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(enemyStats.damage);
        }
    }
}
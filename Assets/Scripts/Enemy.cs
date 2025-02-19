using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum Phase {
        Patrol,
        Chase
    }
    [SerializeField] private EnemyStats enemyStats;
    private Transform player;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private LayerMask obstacleLayer;
    private int currentPatrolIndex = 0;

    private Phase currentPhase;
    public EnemyStats GetEnemyStats()
    {
        return enemyStats;
    }

    public void SetEnemyStats(EnemyStats newStats)
    {
        enemyStats = newStats;
    }
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
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < enemyStats.chaseRange)
        {
            currentPhase = Phase.Chase;
        }
        else
        {
            currentPhase = Phase.Patrol;
        }
        MoveEnemy();
    }

    private void MoveEnemy() {
        Vector2 direction = Vector2.zero;
        if (currentPhase == Phase.Patrol)
        {
            direction = Patrol();
        }
        else if (currentPhase == Phase.Chase)
        {
            direction = Pursue();
        }
        direction = AvoidObstacles(direction);
        transform.position += (Vector3)direction * enemyStats.speed * Time.deltaTime;
    }

    private Vector2 Patrol()
    {
        if (patrolPoints.Length == 0) return Vector2.zero;

        Vector2 targetPosition = patrolPoints[currentPatrolIndex].position;
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

        return direction;
    }

    private Vector2 Pursue()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        return direction;
    }

    private Vector2 AvoidObstacles(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, enemyStats.fieldOfView, obstacleLayer);

        if (hit.collider != null)
        {
            Vector2 avoidanceDirection = Vector2.Perpendicular(hit.normal).normalized;
            return avoidanceDirection;
        }
        return direction;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    enum Phase {
        Patrol,
        Chase
    }
    [SerializeField] private EnemyStats enemyStats;

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
         
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}

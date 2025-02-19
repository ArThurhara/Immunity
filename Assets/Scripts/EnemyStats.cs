using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new EnemyStats", menuName = "Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public float health;
    public float speed;
    public float dropProbability;
    public float fieldofView;
    public float damage;
    public Transform[] patrolPoints;
}

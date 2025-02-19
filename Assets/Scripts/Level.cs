using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Level", menuName = "Levels")]
public class Level : ScriptableObject
{
    public string levelName;
    public string levelID;
    public float gameDuration;
    public int[] enemiesPerStages;
    public GameObject[] enemiesPrefabs;
    public float[] completionStages; // What does threShold mean
    // I'm still looking for a good way to represent the phases for each level
}

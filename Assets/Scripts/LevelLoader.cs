using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Level level;
    private void Start()
    {
        if (GameManager.Instance != null && level != null)
        {
            GameManager.Instance.InitLevel(level);
        }
        else
        {
            Debug.LogError("GameManager or Level not found!");
        }
    }
}

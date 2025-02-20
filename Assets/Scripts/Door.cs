using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player")) {
            SceneManager.LoadScene("Map1");
        }
    }
}

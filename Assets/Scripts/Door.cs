using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string scene_name;
    public string door_id;
    public bool used = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player")) {
            used = true;
        }
    }
    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player")) {
            used = false;
        }
    }
}

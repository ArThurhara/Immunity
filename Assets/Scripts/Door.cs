using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string scene_name;
    public string door_id;
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
            obj.gameObject.GetComponent<PlayerController>().useDoor(door_id);
            SceneManager.LoadScene(scene_name, LoadSceneMode.Single);
        }
    }
}

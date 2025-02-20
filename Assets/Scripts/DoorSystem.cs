using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSystem : MonoBehaviour
{
    private Door[] doors;
    private string player_door;
    public Transform playertf;
    public GameObject Player;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        doors = FindObjectsOfType<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Door door in doors)
        {
            if (door.used && player_door != door.door_id) {
                player_door = door.door_id;
                SceneManager.LoadScene(door.scene_name);
                return;
            } else if (door.used && player_door == door.door_id) {
                return;
            }
        }
        player_door = "";
    }
    void OnSceneLoaded(Scene name, LoadSceneMode mode)
    {
        Debug.Log("New Scene HEHE");
        doors = FindObjectsOfType<Door>();
        foreach (Door door in doors)
        {
            if (door.door_id == player_door)
            {
                Vector3 pos = door.GetComponent<Transform>().position;
                pos.z = playertf.position.z;
                playertf.position = pos;
            }
        }
    }
}

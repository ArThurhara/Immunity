using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public string scene_name = "";
    public void SwithScene() {
        SceneManager.LoadScene(scene_name);
    }
}

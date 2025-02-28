using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnClickSwitchScene : MonoBehaviour
{
    private Button button;
    public string scene_name = "";
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        SceneManager.LoadScene(scene_name);
    }
    public void SwithScene() {
        SceneManager.LoadScene(scene_name);
    }
}

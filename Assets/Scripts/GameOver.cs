using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private Button button;
    int dnasCollected = 0;
    int stagesCleared = 0;

    public TextMeshProUGUI dnasText;
    public TextMeshProUGUI stagesText;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            int[] gameResults = GameManager.Instance.gameResults;
            if (gameResults != null && gameResults.Length >= 2) {
                dnasCollected = gameResults[0];
                stagesCleared = gameResults[1];
                Debug.Log("DNAs: " + dnasCollected);
                dnasText.text = $"Collected: {dnasCollected.ToString()} DNAs";
                stagesText.text = $"Cleared: {stagesCleared.ToString()} Stages";
            }
        }
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

    }
    void OnClick()
    {
        Debug.Log("Clicked");
        SceneManager.LoadScene("Main Menu");
    }
}

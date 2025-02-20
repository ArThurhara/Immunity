using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject panel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.tag == "Player") {
            panel.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collider) {
        if (collider.gameObject.tag == "Player") {
            panel.SetActive(false);
        }
    }
}

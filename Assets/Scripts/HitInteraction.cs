using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitInteraction : MonoBehaviour
{
    BoxCollider2D bc;
    public float dur;
    public float hard;
    public string[] valid;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (dur == 0) Destroy(this);
    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        foreach (string tag in valid) {
            if (!obj.collider.CompareTag(tag)) continue;
            HitInteraction hi = obj.collider.GetComponent<HitInteraction>();
            try {
                dur -= hi.hard;
            } catch {
                Debug.Log("Invalid Hit Interaction: Undefined");
            }
        }
    }
}

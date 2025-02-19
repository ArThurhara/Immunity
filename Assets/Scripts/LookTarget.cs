using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTarget : MonoBehaviour
{
    // Start is called before the first frame update
    public bool lookMouse;
    public GameObject targetGO;
    public Transform target;
    Vector3 myPos;
    Vector3 mousePos;
    void Start()
    {
        if (targetGO) {

            target = targetGO.transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myPos = GetComponent<Transform>().position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir;
        Debug.Log("Begin -------------------------------------------");
        Debug.Log("lookMouse " + lookMouse);
        if (lookMouse)
        {
            dir = (mousePos - myPos).normalized;
        } else {
            Debug.Log("target");
            dir = (target.position - myPos).normalized;
        }
        Debug.Log(dir);
        Debug.Log("End ----------------------------------------------");
    }
}

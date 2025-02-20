using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LookMouse : MonoBehaviour
{
    Transform myTr;
    Vector3 myPos;
    Vector3 mousePos;
    void Start()
    {
        myTr = GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        myPos = myTr.position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir;
        dir = (mousePos - myPos).normalized;
        dir.z = 0;
        float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        myTr.rotation = Quaternion.Euler(0, 0, ang);
        Debug.Log(dir);
    }
}

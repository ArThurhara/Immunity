using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetChaseLinear : MonoBehaviour
{
    // Start is called before the first frame update
    public bool target_is_mouse;
    Vector3 target;
    void Start()
    {
        if (target_is_mouse) {
            var m2 = Input.mousePosition;
            target = Camera.main.ScreenToWorldPoint(m2);
        } else {
            ;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

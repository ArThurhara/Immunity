using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipOnAngle : MonoBehaviour
{
    Transform tf;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (tf.rotation.z > 0 && !sr.flipX) {
            sr.flipX = true;
        } else if (tf.rotation.z < 0 && sr.flipX)  {
            sr.flipX = false;
        }
    }
}

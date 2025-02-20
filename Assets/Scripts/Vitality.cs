using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitality : MonoBehaviour
{
    public float vit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void damage(float dmg)
    {
        vit -= dmg;
        if (vit <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

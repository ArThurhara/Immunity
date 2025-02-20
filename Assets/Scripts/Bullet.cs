using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Vector2 direction;
    public float dmg;
    public float spd = 11f;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 1.5f);
        rb.velocity = direction * spd;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            return;
        }
        try {
            obj.GetComponent<Vitality>().damage(dmg);
        } catch {
        }
        Destroy(this.gameObject);
    }
}

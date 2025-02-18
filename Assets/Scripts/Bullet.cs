using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Vector2 direction;
    private float speed = 15f;

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
    }

    // Update is called once per frame
    private void Update()
    {
        rb.velocity = direction * speed;
    }
}

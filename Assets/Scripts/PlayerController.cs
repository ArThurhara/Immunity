using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    private Vector2 direction;
    private float rotationSpeed = 7f;
    private float angle = 45;

    private bool movingRight = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction = direction.normalized;

        if (direction.x < 0 && !movingRight) {
            flip();
        } else if (direction.x > 0 && movingRight) {
            flip();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    private void flip() {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}

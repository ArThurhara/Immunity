using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private Transform firePoint;
    private Vector2 direction;
    private BulletManager bulletManager;

    private bool movingRight = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletManager = GetComponent<BulletManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction = direction.normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private LayerMask groundLayer; 
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(horizontalInput * _speed * Time.deltaTime, _rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && CheckGround(true))
        {
            float jumpForce = 5.0f;
            
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            CheckGround(false);
        }
    }

    bool CheckGround(bool isGrounded)
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.1f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return isGrounded = true;
        }
        return isGrounded = false;
    }
}

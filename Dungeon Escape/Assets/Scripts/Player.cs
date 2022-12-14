using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private LayerMask _groundLayer;
    private bool _resetJump;
    private bool _grounded;
    private Rigidbody2D _rb;
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordSprite;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetMouseButtonDown(0) && CheckGround() == true)
        {
            _playerAnim.Attack();
        }
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _grounded = CheckGround();

        
        _playerAnim.Move(horizontalInput);

        if (horizontalInput < 0)
        {
            _playerSprite.flipX = true;
            _swordSprite.flipX = true;
            _swordSprite.flipY = true;

            Vector3 newPos = _swordSprite.transform.localPosition;
            newPos.x = -0.7f;
            _swordSprite.transform.localPosition = newPos;
        }
        else if (horizontalInput > 0)
        {
            _playerSprite.flipX = false;
            _swordSprite.flipX = false;
            _swordSprite.flipY = false;

            Vector3 newPos = _swordSprite.transform.localPosition;
            newPos.x = 0.7f;
            _swordSprite.transform.localPosition = newPos;
        }

        Jump();
        _rb.velocity = new Vector2(horizontalInput * _speed * Time.deltaTime, _rb.velocity.y);
    }

    bool CheckGround()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.3f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, _groundLayer);
        Debug.DrawRay(position, direction, Color.green);
        if (hit.collider != null)
        {
            if (_resetJump == false)
            {   
                _playerAnim.Jump(false);
                return true;
            }
        }
        return false;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CheckGround() == true)
        {
            float jumpForce = 5.0f;
            _playerAnim.Jump(true);
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            StartCoroutine(ResetJumpRoutine());
        }
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }
}

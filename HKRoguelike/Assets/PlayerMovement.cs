using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float speed = 5f;
    public float jumpForce = 5f;
    public float gravity = 2f;
    public Vector2 maxSpeed;
    public float maxJumpSpeed;
    public bool facingRight = false;

    [Header("Objects")]
    public Transform feet;

    public Vector2 _velocity;
    private bool _isGrounded = false;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        setGrounded();
        Jump();
        Movement();
        ApplyDirection();
        applyGravity();
        clampVelocity();
        ApplyVelocity();
    }

    void ApplyDirection()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) SetFaceRight(false);
        if (Input.GetKeyDown(KeyCode.A)) SetFaceRight(false);
        if (Input.GetKeyDown(KeyCode.RightArrow)) SetFaceRight(true);
        if (Input.GetKeyDown(KeyCode.D)) SetFaceRight(true);
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _velocity = new Vector2(horizontalInput * speed, _velocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jumping");
            if (_isGrounded)
            {
                Debug.Log("Applying jumpForce");
                _velocity += new Vector2(0, jumpForce);
                _isGrounded = false;
            }
        }
    }

    void ApplyVelocity()
    {
        transform.position += new Vector3(_velocity.x, _velocity.y, 0) * Time.deltaTime;
    }

    public Vector2 GetVelocity()
    {
        return _velocity;
    }

    private void setGrounded()
    {
        if (!_isGrounded)
        {
            RaycastHit2D[] hit = Physics2D.CircleCastAll(feet.position, 0.1f, feet.forward, 1);
            for (int i = 0; i < hit.Length; ++i)
            {
                if (hit[i].collider.tag == "Ground")
                {
                    _isGrounded = true;
                    _velocity.y = 0;
                }
            }
        }
    }

    private void applyGravity()
    {
        if (!_isGrounded)
        {
            _velocity -= new Vector2(0, gravity);
        }
    }

    private void clampVelocity()
    {
        if (_velocity.x < maxSpeed.x * -1) _velocity.x = maxSpeed.x * -1;
        if (_velocity.y < maxSpeed.y * -1) _velocity.y = maxSpeed.y * -1;
        if (_velocity.x > maxSpeed.x) _velocity.x = maxSpeed.x;
        if (_velocity.y > maxJumpSpeed) _velocity.y = maxJumpSpeed;
    }

    private void Dash()
    {
        //TODO implement the dashing part
    }

    /// <summary>
    /// Positions the player to stand left or right
    /// </summary>
    /// <param name="_bool">When true player is facing right, false facing left.</param>
    private void SetFaceRight(bool _bool)
    {
        facingRight = _bool;
        if (facingRight) this.transform.localScale = new Vector3(originalScale.x * -1, originalScale.y, originalScale.z);
        else this.transform.localScale = originalScale;
    }
}
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [Header("Walking")]
    public float speed = 5f;
    public Vector2 maxSpeed;
    public bool facingRight = false;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public float gravity = 2f;
    public float maxJumpSpeed;
    public int amountOfJumps = 2;
    private int jumpsLeft = 0;
    private bool _againstWallLeft = false;
    private bool _againstWallRight = false;

    [Header("Dashing Parameters")]
    public bool dashing = false;
    public float dashTime = 2;
    private float dashTimer = 0;
    public float dashSpeed = 2;

    [Header("Objects")]
    public Transform feet;
    public GameObject playerModel;
    public Vector2 _velocity;
    [SerializeField]private bool _isGrounded = false;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = playerModel.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        ApplyDirection();
        Jump();
        applyGravity();
        clampVelocity();
        Dash();
        ApplyVelocity();
    }

    void ApplyDirection()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) SetFaceRight(false);
        if (Input.GetKey(KeyCode.A)) SetFaceRight(false);
        if (Input.GetKey(KeyCode.RightArrow)) SetFaceRight(true);
        if (Input.GetKey(KeyCode.D)) SetFaceRight(true);
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _velocity = new Vector2(horizontalInput * speed, _velocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) | Input.GetKeyDown(KeyCode.K))
        {
            if (!_isGrounded)
            {
                if (_againstWallLeft)
                {
                    _velocity = new Vector2(3, jumpForce);
                    SetFaceRight(true);
                    return;
                }
                if (_againstWallRight)
                {
                    _velocity = new Vector2(-3, jumpForce);
                    SetFaceRight(false);
                    return;
                }
            }
            if (jumpsLeft > 0)
                {
                    _velocity = new Vector2(_velocity.x, jumpForce);
                    _isGrounded = false;
                    --jumpsLeft;
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

    private void applyGrounded()
    {
        _velocity.y = 0;
        jumpsLeft = amountOfJumps;
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
        if (Input.GetKeyDown(KeyCode.L) && dashing == false)
        {
            dashing = true;
            dashTimer = dashTime;
        }
        if (dashTimer > 0)
        {
            if (facingRight) _velocity = new Vector2(dashSpeed, 0);
            else _velocity = new Vector2(-dashSpeed, 0);

            dashTimer -= Time.deltaTime;
        }
        else
        {
            dashing = false;
        }
    }


    /// <summary>
    /// Positions the player to stand left or right
    /// </summary>
    /// <param name="_bool">When true player is facing right, false facing left.</param>
    private void SetFaceRight(bool _bool)
    {
        if (dashing) return;
        facingRight = _bool;
        if (facingRight) playerModel.transform.localScale = new Vector3(originalScale.x * -1, originalScale.y, originalScale.z);
        else playerModel.transform.localScale = originalScale;
    }


    public void SetGrounded(bool value)
    {
        _isGrounded = value;
        if (_isGrounded) applyGrounded();
    }

    public void SetWallLeft(bool value)
    {
        _againstWallLeft = value;
    }

    public void SetWallRight(bool value)
    {
        _againstWallRight = value;
    }
}
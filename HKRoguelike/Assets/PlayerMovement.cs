using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    private bool _inSpecialMovement = false;
    private float _specialMovementTimer = 0;
    [Header("Walking")]
    public float speed = 5f;
    public Vector2 maxSpeed;
    public bool facingRight = false;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public float gravity = 2f;
    public float maxJumpSpeed;
    public int amountOfJumps = 2;
    public float _wallJumpSpeed = 4;
    public float _wallJumpHeight = 5;
    private int jumpsLeft = 0;
    private bool _againstWallLeft = false;
    private bool _againstWallRight = false;
    public float _wallJumpTime = .1f;

    [Header("Dashing Parameters")]
    public float dashTime = 2;
    public float dashSpeed = 2;

    [Header("Objects")]
    public Transform feet;
    public GameObject playerModel;
    public Vector2 _velocity;
    [SerializeField] private bool _isGrounded = false;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = playerModel.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (_inSpecialMovement)
        {
            _specialMovementTimer -= Time.deltaTime;
            if (_specialMovementTimer <= 0) _inSpecialMovement = false;
        }
        else
        {
            if (CheckSpecialMovementInput()) return;
            Movement();
            ApplyDirection();
            Jump();
            applyGravity();
            clampVelocity();
        }
        ApplyVelocity();
    }
    bool CheckSpecialMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SpecialMovement(SpecialMovementTypes.DASH);
            return true;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!_isGrounded)
            {
                if (_againstWallLeft)
                {
                    SpecialMovement(SpecialMovementTypes.WALLJUMPRIGHT);
                    return true;
                }
                if (_againstWallRight)
                {
                    SpecialMovement(SpecialMovementTypes.WALLJUMPLEFT);
                    return true;
                }
            }
        }


        return false;

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
            if (jumpsLeft > 0)
            {
                _velocity = new Vector2(_velocity.x, jumpForce);
                _isGrounded = false;
                --jumpsLeft;
            }
        }
    }

    void WallJump(bool jumpRight)
    {
        if (jumpRight)
        {
            _velocity = new Vector2(_wallJumpSpeed, _wallJumpHeight);
            SetFaceRight(true);
        }
        else
        {
            _velocity = new Vector2(-_wallJumpSpeed, _wallJumpHeight);
            SetFaceRight(false);
        }
        _specialMovementTimer = 0.2f;
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
        if (_velocity.x > maxSpeed.x) _velocity.x = maxSpeed.x;

        if (_velocity.y < maxSpeed.y * -1) _velocity.y = maxSpeed.y * -1;
        if (_velocity.y > maxJumpSpeed) _velocity.y = maxJumpSpeed;

        if (_isGrounded && _velocity.y < 0) _velocity.y = 0;

        if (_againstWallLeft && _velocity.x < 0) _velocity.x = 0;
        if (_againstWallRight && _velocity.x > 0) _velocity.x = 0;
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            _specialMovementTimer = dashTime;
            if (facingRight) _velocity = new Vector2(dashSpeed, 0);
            else _velocity = new Vector2(-dashSpeed, 0);
        }
    }

    /// <summary>
    /// Positions the player to stand left or right
    /// </summary>
    /// <param name="_bool">When true player is facing right, false facing left.</param>
    private void SetFaceRight(bool _bool)
    {
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
    public void SpecialMovement(SpecialMovementTypes type)
    {
        _inSpecialMovement = true;
        switch (type)
        {
            case SpecialMovementTypes.DASH:
                Dash();
                break;
            case SpecialMovementTypes.WALLJUMPLEFT:
                WallJump(false);
                break;
            case SpecialMovementTypes.WALLJUMPRIGHT:
                WallJump(true);
                break;
        }
    }
    public bool isGrounded()
    {
        return _isGrounded;
    }
}

public enum SpecialMovementTypes
{
    DASH,
    WALLJUMPLEFT,
    WALLJUMPRIGHT,
}
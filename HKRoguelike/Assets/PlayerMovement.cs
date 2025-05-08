using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float speed = 5f;
    public float jumpForce = 5f;
    public float gravity = 2f;
    public Vector2 maxSpeed;

    [Header("Objects")]
    public Transform feet;

    private Vector2 _velocity;
    private bool _isGrounded = false;

    // Update is called once per frame
    void Update()
    {
        setGrounded();
        Movement();
        Jump();
        applyGravity();
        clampVelocity();
        ApplyVelocity();
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _velocity = new Vector2(horizontalInput, 0) * speed;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isGrounded)
            {
                _velocity = new Vector2(_velocity.x, jumpForce);
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
        RaycastHit hit;
        if (Physics.SphereCast(feet.position, 0.1f, feet.forward, out hit, 1))
        {
            Debug.Log(hit.collider.tag);
            if (hit.collider.tag == "Ground")
            {
                if (!_isGrounded)
                {
                    _isGrounded = true;
                    _velocity.y = 0;
                    Debug.Log("Setting grounded");
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
        if (_velocity.y > maxSpeed.y) _velocity.y = maxSpeed.y;
    }
}
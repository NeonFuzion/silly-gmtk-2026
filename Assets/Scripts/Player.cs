using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float runAccelRate, airborneAccelRate, runDecelRate, airborneDecelRate, maxRunSpeed;
    [SerializeField] float jumpForce, jumpReleaseMultiplier, coyoteTime, jumpBuffer;

    float accelRate, decelRate, currentJumpBuffer, currentCoyoteTime;

    Vector2 movement, respawnPosition;
    new Rigidbody2D rigidbody;
    BoxCollider2D boxCollider;
    PlayerState playerState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        respawnPosition = transform.position;

        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        JumpCheck();
    }

    bool IsGrounded()
    {
        Vector3 offset = Vector3.down * boxCollider.size.y / 2;
        return Physics2D.OverlapCircle(transform.position + offset, 0.2f, LayerMask.GetMask("Ground"));
    }

    void Movement()
    {
        accelRate = playerState == PlayerState.Grounded ? runAccelRate : airborneAccelRate;
        decelRate = playerState == PlayerState.Grounded ? runDecelRate : airborneDecelRate;
        float targSpd = movement.x * maxRunSpeed;
        float currentAccelRate = Mathf.Abs(targSpd) > 0.01f ? accelRate : decelRate;

        float speedDif = targSpd - rigidbody.linearVelocityX;
        float horizontal = speedDif * currentAccelRate * Time.deltaTime;
        rigidbody.AddForce(horizontal * Vector2.right, ForceMode2D.Force);
    }
    
    void JumpCheck()
    {
        if (IsGrounded())
        {
            if (playerState != PlayerState.Grounded && currentJumpBuffer > 0)
            {
                currentJumpBuffer = 0;
                playerState = PlayerState.Grounded;
                Jump(InputActionPhase.Started);
            }
            if (playerState != PlayerState.Jumping)
            {
                playerState = PlayerState.Grounded;
            }
        }
        else
        {
            if (rigidbody.linearVelocityY >= 0) return;
            playerState = PlayerState.Airborne;

            if (playerState != PlayerState.Grounded) return;
            currentCoyoteTime = coyoteTime;
        }
    }

    void Jump(InputActionPhase phase)
    {
        if (phase == InputActionPhase.Started)
        {
            if (playerState == PlayerState.Grounded || (currentCoyoteTime > 0 && playerState == PlayerState.Airborne))
            {
                rigidbody.linearVelocity = new(rigidbody.linearVelocity.x, 0);
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
                currentCoyoteTime = 0;
                playerState = PlayerState.Jumping;
            }
            else
            {
                currentJumpBuffer = jumpBuffer;
            }
        }
        else if (phase == InputActionPhase.Canceled)
        {
            if (rigidbody.linearVelocity.y > 0)
                rigidbody.linearVelocity = new(rigidbody.linearVelocity.x, rigidbody.linearVelocity.y * jumpReleaseMultiplier);
        }
    }

    public void RespawnPlayer()
    {
        transform.position = respawnPosition;
    }

    public void SetRespawn(Transform respawn)
    {
        respawnPosition = respawn.position;
    }

    public void MovementInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        movement = input;
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        Jump(context.phase);
    }

    enum PlayerState { None, Grounded, Jumping, Airborne }
}

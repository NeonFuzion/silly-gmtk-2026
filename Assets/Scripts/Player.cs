using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float runAccelRate, airborneAccelRate, runDecelRate, airborneDecelRate, maxRunSpeed;
    [SerializeField] float jumpForce, jumpReleaseMultiplier, coyoteTime, jumpBuffer;
    [SerializeField] Transform sun;

    float accelRate, decelRate, currentJumpBuffer, currentCoyoteTime;

    Vector2 movement;
    new Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;
    PlayerState playerState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Burn();
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

    void Burn()
    {
        Vector2 distanceVector = sun.position - transform.position;
        if (Physics2D.Raycast(transform.position, distanceVector, distanceVector.magnitude, LayerMask.GetMask("Ground")))
        {
            spriteRenderer.color = Color.white;
        }
        else
        {

            spriteRenderer.color = Color.red;
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

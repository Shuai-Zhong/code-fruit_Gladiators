using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    private float jumpForce;
    private float movementSmoothing;
    private bool airControl;
    private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    private float groundedRadius; // Radius of the overlap circle to determine if grounded
    private Rigidbody2D rigidBody2D;
    private bool isFacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 currentVelocity;

    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        jumpForce = 500f;
        movementSmoothing = 0.15f;
        groundedRadius = 0.2f;
        currentVelocity = Vector3.zero;
        airControl = true;
    }

    private void FixedUpdate()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }
    }

    public void Move(float move, bool jump)
    {
        //only control the player if grounded or airControl is turned on
        if (isGrounded || airControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, rigidBody2D.velocity.y);
            rigidBody2D.velocity = Vector3.SmoothDamp(rigidBody2D.velocity, targetVelocity, ref currentVelocity, movementSmoothing);

            // If the input is moving the player right and the player is facing left
            if (move > 0 && !isFacingRight)
            {
                // flip the player
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right
            else if (move < 0 && isFacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump
        if (isGrounded && jump)
        {
            // Add a vertical force to the player
            isGrounded = false;
            rigidBody2D.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing
        isFacingRight = !isFacingRight;

        // Multiply the player's x local scale by -1 to flip
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Get grounded state
    public bool GetGroundedState()
    {
        return isGrounded;
    }

    // Get velocity of rigid body
    public float GetRBVelocity()
    {
        return rigidBody2D.velocity.y;
    }
}
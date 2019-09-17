using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private ScreenShake screenShake;
    private Audiomanager AM;
    private GameManager GM;
    private float horizontalMove;
    private float velocity;
    private bool jump;
    private bool isLongFall;
    private float runSpeed;

    protected Joystick joystick;
    protected JoyButton joyButtonX;
    protected JoyButton joyButtonA;

    public CharacterController2D controller;
    public Animator animator;
    public ItemHandling itemHandling;
    public ParticleSystem medParticle;
    public GameObject groundCheck;
    public PauseMenu pauseMenu;

    private void Awake()
    {
        screenShake = FindObjectOfType<ScreenShake>();
        AM = FindObjectOfType<Audiomanager>();
        GM = FindObjectOfType<GameManager>();
        pauseMenu = FindObjectOfType<PauseMenu>();

        joystick = FindObjectOfType<Joystick>();

        // Enable mobile controls on Android or IOS
        #if UNITY_ANDROID || UNITY_IOS
            joyButtonA = FindObjectsOfType<JoyButton>()[0];
            joyButtonX = FindObjectsOfType<JoyButton>()[1];
        #endif
    }

    private void Start()
    {
        animator.SetBool("IsDying", false);
        jump = false;
        horizontalMove = 0f;
        runSpeed = 70f;

        // Sets up player fall in scene
        animator.Play("Player_Descending");
        isLongFall = true;
    }

    private void Update()
    {
        // Pause Game on Button press
        if (Input.GetButtonDown("Exit"))
        {
            pauseMenu.PressPause();
        }

        // Cut off all inputs when paused
        if (pauseMenu.GetPausedState())
        {
            return;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        // Add the joystick controls if it exists
        if (joystick)
        {
            horizontalMove += joystick.Horizontal * runSpeed;
        }

        // Set speed and velocity on animator
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetFloat("VelocityVert", controller.GetRBVelocity());

        // Handle Jump
        if (Input.GetButtonDown("Jump") && controller.GetGroundedState())
        {
            Jump();
        }

        // Throw the item upward when pressing the button
        if (Input.GetButtonDown("Throw"))
        {
            itemHandling.ThrowItem();
        }
    }

    private void FixedUpdate()
    {
        // Pass on move data to other script and resets jump
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }

    // Handle collision with Screen shake when hitting ground
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && isLongFall)
        {
            StartCoroutine(screenShake.Shake(0.10f, 0.1f));
            Instantiate(medParticle, groundCheck.transform);
            AM.Play("ExplosionSmall");
            isLongFall = false;
        }
    }

    // Reset booleans in the animator upon landing
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    // Handle die animation and sets game state
    public void Die()
    {
        gameObject.layer = 12;
        animator.SetBool("IsDying", true);
        GetComponent<SpriteRenderer>().sortingOrder = 10;

        GM.Invoke("GameOver", 1);
    }

    // Let player character jump
    private void Jump()
    {
        jump = true;
        animator.SetBool("IsJumping", true);
        AM.Play("Jump");
        isLongFall = true;
    }

    // Handle button input in mobile mode
    public void HandlePointerPress(string buttonName)
    {
        switch (buttonName)
        {
            case "ButtonX":
                itemHandling.ThrowItem();
                break;

            case "ButtonA":
                if (controller.GetGroundedState())
                {
                    Jump();
                }
                break;

            case "ButtonPause":
                pauseMenu.PressPause();
                break;

            default:
                break;
        }
    }
}
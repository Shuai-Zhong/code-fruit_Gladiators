using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParameters : MonoBehaviour
{
    private bool wasHeld = false; // Needs to be set here and not in start
    private bool isGrounded;
    private float groundedTimer;
    private float maxGroundedTime;
    private Audiomanager AM;
    private GameManager GM;

    private void Awake()
    {
        GM = FindObjectOfType<GameManager>();
        AM = FindObjectOfType<Audiomanager>();
    }

    private void Start()
    {
        maxGroundedTime = 5f;

        isGrounded = false;
    }

    private void Update()
    {
        CheckGroundedState();
    }

    // Activates isGrounded if fruit hits the floor
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Checks grounded state of fruit
    private void CheckGroundedState()
    {
        // counts up when fruit is grounded
        if (isGrounded)
        {
            groundedTimer += Time.deltaTime;
        }

        // Calls GM to end the game if fruit was on the ground for X seconds
        if (groundedTimer >= maxGroundedTime)
        {
            AM.Play("LoseTwo");
            GM.GameOver();
        }
    }

    // Sets wasHeld when fruit was thrown
    public void SetHeldStatus(bool heldStatus)
    {
        wasHeld = heldStatus; 
    }

    // Gets wasHeld status
    public bool GetHeldStatus()
    {
        return wasHeld;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassHandler : MonoBehaviour
{
    private Animator anim;
    private bool canNotTrigger; // Prevent animation from re-triggering until finished

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start ()
    {
        canNotTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Move grass when it collides with anything other than 'Ground'
        if (!canNotTrigger && !other.gameObject.CompareTag("Ground"))
        {
            anim.SetTrigger("Move");
            canNotTrigger = true;
        }
    }

    // Accessed from animator to allow grass to move again
    public void ResetAnim()
    {
        canNotTrigger = false;
    }
}

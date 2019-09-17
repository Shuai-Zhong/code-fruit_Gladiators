using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceBlockHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem hvyParticle;
    private ScreenShake screenShake;
    private Audiomanager AM;
    private float blockDestructionDelay;

    private void Awake()
    {
        screenShake = FindObjectOfType<ScreenShake>();
        AM = FindObjectOfType<Audiomanager>();
    }

    private void Start()
    {
        blockDestructionDelay = 2.0f;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Activate particles and screen shake when colliding with the ground
        if (other.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(screenShake.Shake(0.15f, 0.4f));
            AM.Play("ExplosionBig");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Instantiate(hvyParticle, transform);
            Invoke("DestroyBlock", blockDestructionDelay);
        }
        // Kill player on collision
        else if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerScript = other.gameObject.GetComponent<PlayerMovement>();
            AM.Play("LoseTwo");
            playerScript.Die();
        }
    }

    // Self destruct
    private void DestroyBlock()
    {
        Destroy(gameObject);
    }
}

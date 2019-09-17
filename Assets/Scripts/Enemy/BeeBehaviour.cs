using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBehaviour : MonoBehaviour
{
    private Audiomanager AM;

    private float timeCounter;
    private float movement;
    private float speed;
    private float width;
    private float height;
    private float movementSpeed;

    private void Awake()
    {
        AM = FindObjectOfType<Audiomanager>();
    }

    private void Start()
    {
        timeCounter = 0;
        movement = 0;
        SetInitValues();

        Invoke("SelfDestruct", 20f);

        AM.Play("BeeHive");
    }

    private void Update()
    {
        timeCounter += Time.deltaTime * speed;

        MoveCharacter();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerScript = other.gameObject.GetComponent<PlayerMovement>();
            AM.Play("LoseTwo");
            playerScript.Die();
        }
    }

    // Set initial values
    private void SetInitValues()
    {
        width = 3;
        height = 3;
        speed = 2;

        // Get spawn position and decide move direction
        float startPos = gameObject.transform.position.x;
        if(startPos < 0)
        {
            movementSpeed = 2.5f;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            movementSpeed = -2.5f;
        }
    }

    // Update character position
    private void MoveCharacter()
    {
        movement += Time.deltaTime * movementSpeed;
        float x = Mathf.Cos(timeCounter) * width + movement;
        float y = Mathf.Sin(timeCounter) * height;

        transform.localPosition = new Vector3(x, y, 0);
    }

    // Self destruct
    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}

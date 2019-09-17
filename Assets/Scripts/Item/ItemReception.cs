using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReception : MonoBehaviour
{
    private Animator anim;
    private Audiomanager AM;
    private ScoreManager scoreManager;

    public GameObject floatingScore;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        AM = FindObjectOfType<Audiomanager>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Checks if the tag of the bush and fruit are a match
        if
        (
        gameObject.CompareTag("CherryBush") && other.gameObject.CompareTag("Cherry") ||
        gameObject.CompareTag("AvocadoBush") && other.gameObject.CompareTag("Avocado") ||
        gameObject.CompareTag("EggplantBush") && other.gameObject.CompareTag("Eggplant")
        )
        {
            // Gets wether or not the triggering item was thrown by the player
            bool itemWasHeld = other.gameObject.GetComponent<ItemParameters>().GetHeldStatus();

            // Gets velocity of item to determine weather or not it is falling
            float itemVelocity = other.gameObject.GetComponent<Rigidbody2D>().velocity.y;

            // Checks if the item is a fruit
            if (itemWasHeld && itemVelocity < 0)
            {
                AcceptItem(other.gameObject);
                AM.Play("Collect");
            }
        }
    }

    // Accepts the item and destroys it while incrementing the score
    private void AcceptItem(GameObject otherObj)
    {
        anim.SetTrigger("Shake");
        Instantiate(floatingScore, otherObj.transform.position, Quaternion.identity);
        Destroy(otherObj);
        scoreManager.IncrementScore("Item Delivery");
    }
}

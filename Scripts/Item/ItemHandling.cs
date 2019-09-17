using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandling : MonoBehaviour
{
    private ItemDisplay itemDisplay;
    private string heldItem;
    private int tagIndex;

    public GameObject[] fruits;
    public float throwForce;

    private void Awake()
    {
        itemDisplay = FindObjectOfType<ItemDisplay>();
    }

    private void Start()
    {
        heldItem = null;
        throwForce = 10f;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Checks that no item is being held
        if (heldItem == null)
        {
            // Checks if colliding item is a fruit
            if (                                            
                other.gameObject.CompareTag("Cherry") ||    
                other.gameObject.CompareTag("Avocado") ||
                other.gameObject.CompareTag("Eggplant")
            )
            {
                heldItem = other.gameObject.tag;
                itemDisplay.EnableImage(heldItem);
                Destroy(other.gameObject);
            }
        }
    }

    // Throw item
    public void ThrowItem()
    {
        // Only activates when item is being held
        if (heldItem != null) 
        {
            ConvertTagtoNum();
            heldItem = null;
            itemDisplay.DisableImage();

            GameObject thrownItem = Instantiate(fruits[tagIndex], transform.position + new Vector3(0, 1), Quaternion.identity) as GameObject;
            thrownItem.GetComponent<Rigidbody2D>().velocity = thrownItem.transform.up * throwForce;
            thrownItem.GetComponent<ItemParameters>().SetHeldStatus(true);
        }
    }

    // Convert heldItem tag to array index
    private void ConvertTagtoNum() 
    {
        switch (heldItem)
        {
            case "Cherry":
                tagIndex = 0;
                break;

            case "Avocado":
                tagIndex = 1;
                break;
                
            case "Eggplant":
                tagIndex = 2;
                break;
        }
    }
}

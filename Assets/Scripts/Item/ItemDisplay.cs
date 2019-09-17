using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    private Image displayImage;
    private Sprite heldItemSprite;
    [SerializeField] private Sprite[] fruitSprites;

    private void Awake()
    {
        displayImage = GetComponent<Image>();
    }

    private void Start()
    {
        DisableImage();
    }

    // Disable image when no item held
    public void DisableImage()
    {
        displayImage.enabled = false;
    }

    // Enable image if item is held
    public void EnableImage(string spriteName)
    {
        switch (spriteName)
        {
            case "Cherry":
                heldItemSprite = fruitSprites[0];
                break;
            case "Avocado":
                heldItemSprite = fruitSprites[1];
                break;
            case "Eggplant":
                heldItemSprite = fruitSprites[2];
                break;
            default:
                return;
        }

        displayImage.sprite = heldItemSprite;
        displayImage.enabled = true;
    }
}

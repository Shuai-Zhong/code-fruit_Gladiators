using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyButton : MonoBehaviour, IPointerDownHandler
{
    public PlayerMovement playerMov;

    private void Awake()
    {
        playerMov = FindObjectOfType<PlayerMovement>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        playerMov.HandlePointerPress(gameObject.name);
    }
}

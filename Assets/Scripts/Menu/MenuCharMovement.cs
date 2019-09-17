using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCharMovement : MonoBehaviour
{
    // Background character movement speed
    private float speed;

	void Start ()
    {
        speed = 2.5f;
        Invoke("SelfDestruct", 20f);
	}
	
	void Update ()
    {
        UpdatePosition();
	}

    // Update background character position
    private void UpdatePosition()
    {
        float moveX = Time.deltaTime * speed;
        Vector3 newPos = new Vector3(moveX, 0);
        gameObject.transform.position -= newPos;
    }

    // Destroys gameobject
    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}

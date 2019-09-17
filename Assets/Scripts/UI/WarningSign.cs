using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSign : MonoBehaviour
{
    private float destructionDelay;

	void Start ()
    {
        destructionDelay = 0.6f;
        Invoke("SelfDestruct", destructionDelay);
	}
    
    // Self destruct
    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}

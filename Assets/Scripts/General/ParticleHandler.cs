using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
	void Start ()
    {
        Invoke("DestroyParticle", 2);
	}
	
    // Destroy particle effects after they run out of use
    private void DestroyParticle()
    {
        Destroy(gameObject);
    }
}

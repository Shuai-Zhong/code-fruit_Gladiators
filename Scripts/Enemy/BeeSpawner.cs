using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject beePrefab;
    private float timer;
    private float beeReset;

	private void Start ()
    {
        timer = 0;
        beeReset = 10f;

        InvokeRepeating("RollSpawn", 10f, 12f);
	}
	
	private void Update ()
    {
        timer += Time.deltaTime;
	}

    // Decide to spawn a bee or not
    private void RollSpawn()
    {
        int roll = Random.Range(1, 2);
        
        if(roll == 1 && timer >= beeReset)
        {
            SpawnBee();
        }
    }

    // Spawn a bee
    private void SpawnBee()
    {
        int rollX = Random.Range(0, 2); // 33.3% chance
        int posX;

        // Spawn left or right
        if (rollX == 0)
        {
            posX = 15;
        }
        else
        {
            posX = -15;
        }

        // Decide spawn height in range
        int posY = Random.Range(-2, 2);

        Vector2 spawnPos = new Vector2(posX, posY);
        Instantiate(beePrefab, spawnPos, Quaternion.identity);
        timer = 0;
    }
}

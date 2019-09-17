using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    private float minSpawnX;
    private float maxSpawnX;
    [SerializeField] private GameObject[] fruits;
    [SerializeField] private GameObject[] enemies;

    public WarningHandler warningHandler;
    public float spawnHeight;

    private void Start()
    {
        minSpawnX = -7.5f;
        maxSpawnX = 7.5f;

        spawnHeight = 8;
    }

    // Spawns a random fruit
    public void SpawnFruit()
    {
        Vector2 spawnPos = new Vector2(DetermineSpawnPos(), spawnHeight); // Sets x and y for spawn position
        Instantiate(fruits[RollIndexNum(fruits.Length)], spawnPos, Quaternion.identity); // Instantiates fruit
    }

    // Spawns a random enemy
    public void SpawnEnemy()
    {
        float spawnHor = DetermineSpawnPos();
        warningHandler.SetWarning(spawnHor);

        Vector2 spawnPos = new Vector2(spawnHor, spawnHeight); // Sets x and y for spawn position
        Instantiate(enemies[RollIndexNum(enemies.Length)], spawnPos, Quaternion.identity); // Instantiates fruit
    }

    // Returns random spawn position within range
    private float DetermineSpawnPos()
    { 
        return Random.Range(minSpawnX, maxSpawnX);
    }

    // Returns random index Number
    private int RollIndexNum(int arrLength)
    {
        return Random.Range(0, arrLength);
    }
}

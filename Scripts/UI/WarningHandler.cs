using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningHandler : MonoBehaviour
{
    private Vector3 spawnPos;
    private GameManager GM;
    private float delay;

    public GameObject warningSign;

    private void Awake()
    {
        GM = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        delay = GM.warningDelay;
    }

    // Prepare warning sign
    public void SetWarning(float enemySpawn)
    {
        spawnPos = new Vector3(enemySpawn, 4);
        Invoke("SpawnWarning", delay);
    }

    // Spawn warning sign
    private void SpawnWarning()
    {
        Instantiate(warningSign, spawnPos, Quaternion.identity);
    }
}

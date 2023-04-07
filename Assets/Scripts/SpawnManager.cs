using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] spawnPrefab;
    [SerializeField] private float spawnTime;
    [SerializeField] private int objectCounts;
    void Awake()
    {
        if (objectCounts != 0)
        {
            GetSpawnPoints();
        }
    }

    private void GetSpawnPoints()
    {
        foreach (SpawnPoint spawnPoint in FindObjectsOfType<SpawnPoint>())
        {
            spawnPoint.InitSpawnObjects(spawnPrefab, spawnTime, objectCounts);
        }
    }
}

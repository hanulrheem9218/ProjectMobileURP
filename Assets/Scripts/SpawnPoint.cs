using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float fieldSize;

    private List<Transform> spawnLocations = new List<Transform>();
    [Header("Enemy Type")]
    [SerializeField] bool isEnemy;
    [SerializeField] int totalGameObjects;

    private GameObject[] enemyPrefab;
    [SerializeField] private Color gizmosColor;
    // void Start()
    // {
    //     Invoke(nameof(SpawnObjects), spawnTime);
    // }
    public void InitSpawnObjects(GameObject[] prefab, float spawnTime, int totalGameObjects)
    {
        if (prefab != null)
        {
            this.totalGameObjects = totalGameObjects;
            this.enemyPrefab = prefab;
            Invoke(nameof(SpawnObjects), spawnTime);
        }
    }
    private void SpawnObjects()
    {
        for (int i = 0; i < totalGameObjects; i++)
        {
            var spawnGameObejct = new GameObject();
            spawnGameObejct.name = "ObjectSpawnPoint" + i;
            spawnGameObejct.transform.SetParent(transform);
            Transform spawnLocation = spawnGameObejct.transform;
            spawnLocations.Add(spawnLocation);
            //calcualte random position;
            float spawnX = Random.Range(-fieldSize, fieldSize);
            float spawnZ = Random.Range(-fieldSize, fieldSize);
            spawnLocations[i].transform.position = Vector3.zero;
            spawnLocations[i].transform.position = new Vector3(spawnX + transform.position.x, transform.position.y, spawnZ + transform.position.z);

            if (spawnLocations[i].transform.childCount <= 0)
            {
                int randomSpawn = Random.Range(0, enemyPrefab.Length);
                Transform spawnPrefab = Instantiate(this.enemyPrefab[randomSpawn].transform, spawnLocations[i].transform);
                spawnPrefab.name = "spawnObject" + i;
                // you can set things out here.
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireCube(transform.position, new Vector3((fieldSize) * 2, 0.5f, (fieldSize) * 2));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public List<Transform> spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnRandomObject()
    {
        int randomObject = Random.Range(0, gameObjects.Count);
        int randomSPawnPoint = Random.Range(0, spawnPoints.Count);

        GameObject spawnObject = gameObjects[randomObject];
        Instantiate(spawnObject, spawnPoints[randomSPawnPoint], true);
    }
}

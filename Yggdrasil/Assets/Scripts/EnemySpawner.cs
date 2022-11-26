using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject resourcePrefab;
    [SerializeField]public List<GameObject> spawnedEnemy;
    [SerializeField]public List<GameObject> spawnedResource;

    private float enemySpeed;
    private float resourceSpeed;
    private float resourceSpawnTime;
    [SerializeField]private int numEnemy;
    [SerializeField]private int numResource;
    private float screenHeight;
    private float screenWidth;

    private void Start()
    {
        enemySpeed = 1.0f;
        resourceSpeed = 1.0f;
        resourceSpawnTime = 2f;
        spawnedEnemy = new List<GameObject>();
        spawnedResource = new List<GameObject>();
        var screenSpace = new Vector2(Screen.width, Screen.height);
        if (Camera.main != null)
        {
            var workSpace = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            screenWidth = workSpace.x;
            screenHeight = workSpace.y;
        }
        
    }

    private void Update()
    {
        if (spawnedEnemy.Count < numEnemy)
        {
            SpawnEnemy(enemyPrefab, 1); 
        }

        if (spawnedResource.Count < numResource)
        {
            SpawnEnemy(resourcePrefab, 0);
        }
    }

    private void SpawnEnemy(GameObject enemy, int objectType)
    {
        Debug.Log(enemy);
        var edgePosition = Random.Range(0, 2);

        GameObject newEnemy;
        if (edgePosition == 1)
        {
            newEnemy = Instantiate(enemy, new Vector3(Random.Range(0, 2)*screenWidth, Random.Range(0, screenHeight), 0), Quaternion.identity);
        }
        else
        {
            newEnemy = Instantiate(enemy, new Vector3(Random.Range(0, screenWidth), Random.Range(0, 2)*screenHeight, 0), Quaternion.identity);
        }

        if (objectType == 1)
        {
            spawnedEnemy.Add(newEnemy);
        }
        else
        {
            spawnedResource.Add(newEnemy);
        }
    }

    // private Vector3 GetSpawnPosition()
    // {
    //     
    // }
}

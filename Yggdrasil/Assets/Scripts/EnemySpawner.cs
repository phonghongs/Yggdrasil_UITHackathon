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

    [SerializeField] private float fixedTimeEnemy;
    [SerializeField] private float fixedTimeResource;
    [SerializeField] private float percentTimeEnemy;
    [SerializeField] private float percentTimeResource;
    [SerializeField] private float scalePerTime;
    private float enemyTimeSpawn;
    private float resourceTimeSpawn;
    private float scaleTimeSpawn;
    private float screenHeight;
    private float screenWidth;
    private void Start()
    {
        enemyTimeSpawn = 0;
        resourceTimeSpawn = 0;
        spawnedEnemy = new List<GameObject>();
        spawnedResource = new List<GameObject>();
        var screenSpace = new Vector2(Screen.width, Screen.height);
        UpdateScreenSize(0);
    }

    private void Update()
    {
        var gameTime = Time.realtimeSinceStartup;
        if (gameTime - enemyTimeSpawn > fixedTimeEnemy)
        {
            // Debug.Log("Enemy");
            enemyTimeSpawn = gameTime;
            SpawnEnemy(enemyPrefab, ObjectType.ENEMY); 
        }
        
        if (gameTime - resourceTimeSpawn > fixedTimeResource)
        {
            // Debug.Log("Resource");
            resourceTimeSpawn = gameTime;
            SpawnEnemy(resourcePrefab, ObjectType.RESOURCE);
        }

        if (gameTime - scaleTimeSpawn > scalePerTime)
        {
            scaleTimeSpawn = gameTime;
            fixedTimeEnemy *= (1 - percentTimeEnemy);
            fixedTimeResource *= (1 - percentTimeResource);
        }
    }

    private void SpawnEnemy(GameObject enemy, ObjectType objectType)
    {
        var edgePosition = Random.Range(0, 2);

        var newEnemy = Instantiate(enemy, GetSpawnPosition(), Quaternion.identity);
        
        if (objectType == ObjectType.ENEMY)
        {
            spawnedEnemy.Add(newEnemy);
        }
        else
        {
            spawnedResource.Add(newEnemy);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        var edgePositionWidth = Random.Range(0, 2);
        var edgePositionHeight = Random.Range(0, 2);
        if (edgePositionHeight == 0)
        {
            edgePositionHeight = -1;
        }
        Debug.Log($"Screen: {screenWidth} - {screenHeight} || {edgePositionHeight * screenHeight} || {edgePositionHeight * screenWidth}");
        if (edgePositionWidth == 0)
        {
            return new Vector3(Random.Range(-screenWidth, screenWidth), edgePositionHeight * screenHeight, 0);
        }
        else
        {
            return new Vector3(edgePositionHeight * screenWidth, Random.Range(-screenWidth, screenHeight), 0);
        }
    }

    public void UpdateScreenSize(float scaleValue)
    {
        Debug.LogError($"{Screen.width} || {Screen.height}");
        Camera main = Camera.main;
        if (main != null)
        {
            main.orthographicSize += scaleValue;
            var workSpace = main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            screenWidth = workSpace.x;
            screenHeight = workSpace.y;
        }
    }
}

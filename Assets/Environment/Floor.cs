using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Floor : MonoBehaviour
{
    public static Floor S;
    public GameObject enemyPrefab;

    public float minSpawnX;
    public float maxSpawnX;
    public float minSpawnY;
    public float maxSpawnY;
    public float minSpawnZ;
    public float maxSpawnZ;

    public float scaleX;
    public float scaleY;
    public float scaleZ;

    public static GameObject[] enemies;

    public AudioSource _countdownSoundEffect;
    
    private int _level = 0;
    private bool _canSpawnEnemy = true;
    private int _enemiesToSpawn;
    private int _enemiesSpawned;
    
    void Awake()
    {
       // StartCoroutine(StartNextLevel());
        var localScale = gameObject.transform.localScale;

        _countdownSoundEffect = gameObject.GetComponent<AudioSource>();

        _enemiesToSpawn = 10;
        _enemiesSpawned = 0;
        
        scaleX = localScale.x;
        scaleY = localScale.y;
        scaleZ = localScale.z;

        minSpawnX = 0 - (scaleX / 2 - 3);
        maxSpawnX = 0 + (scaleX / 2 - 3);
        minSpawnY = 1.5f;
        maxSpawnY = 1.5f;
        minSpawnZ = 0 - (scaleY / 2 - 3);
        maxSpawnZ = 0 + (scaleY / 2 - 3);
        
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Floor!");
        }
    }

    private void FixedUpdate()
    {
        if (Random.Range(0, 100) < 5 && _canSpawnEnemy)
        {
            SpawnEnemyAtRandomLocation();
            _enemiesSpawned++;
        };
    }
    
    private void SpawnEnemyAtRandomLocation()
    {
        Instantiate(enemyPrefab, GenerateRandomLocation(), Quaternion.identity);
    }

    private Vector3 GenerateRandomLocation()
    {
        var randomXPos = Random.Range(minSpawnX, maxSpawnX);
        var randomYPos = Random.Range(minSpawnY, maxSpawnY);
        var randomZPos = Random.Range(minSpawnZ, maxSpawnZ);

        return new Vector3(randomXPos, 0f, randomZPos);
    }

    private void DestroyAll()
    {
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    IEnumerator StartNextLevel()
    {
        _enemiesSpawned = 0;
        _level += 1;
        _countdownSoundEffect.Play();
        yield return new WaitForSeconds(10);
        _canSpawnEnemy = true;
        _countdownSoundEffect.Stop();
    }
}

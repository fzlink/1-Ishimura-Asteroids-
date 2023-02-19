using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private int AsteroidCap = 8;
    [SerializeField] private PlayerController Player;
    
    private Vector2 _minBound;
    private Vector2 _maxBound;

    private List<Asteroid> Asteroids;

    private void Awake()
    {
        Asteroids = new List<Asteroid>(AsteroidCap);
    }

    void Start()
    {
        _minBound = Camera.main.ScreenToWorldPoint(Vector2.zero);
        _maxBound = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (Asteroids.Count < AsteroidCap)
            {
                var asteroid = (Asteroid) AsteroidPool.Instance.GetObject();
                asteroid.gameObject.SetActive(true);
                asteroid.SetAsteroidModel(3);
                Asteroids.Add(asteroid);
                var spawnPoint = SelectRandomSpawnPoint();
                asteroid.transform.position = spawnPoint;
                var direction = (Vector2)Player.transform.position - spawnPoint;
                asteroid.Release(direction.normalized);
            }
            yield return new WaitForSeconds(3f);
        }
    }

    private Vector2 SelectRandomSpawnPoint()
    {
        Vector2 randomSpawnPoint = Vector2.zero;
        bool fixedX = UnityEngine.Random.value < 0.5f;
        bool isMin = UnityEngine.Random.value < 0.5f;
        
        if (fixedX)
        {
            if (isMin)
            {
                randomSpawnPoint.x = _minBound.x - 2f;
            }
            else
            {
                randomSpawnPoint.x = _maxBound.x + 2f;
            }

            randomSpawnPoint.y = UnityEngine.Random.Range(_minBound.y - 2f, _maxBound.y + 2f);
        }
        else
        {
            if (isMin)
            {
                randomSpawnPoint.y = _minBound.y - 2f;
            }
            else
            {
                randomSpawnPoint.y = _maxBound.y + 2f;
            }

            randomSpawnPoint.x = UnityEngine.Random.Range(_minBound.x - 2f, _maxBound.x + 2f);
        }

        return randomSpawnPoint;
    }
    
}

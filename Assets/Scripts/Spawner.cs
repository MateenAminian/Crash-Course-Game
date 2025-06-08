using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Prefabs;
    public float MinimumTime = 1.0f;
    public float MaximumTime = 5.0f;

    private float _nextSpawnTime;
    private GameObject _spawnedObject;

    // Start is called before the first frame update
    void Start()
    {
        _nextSpawnTime = CalcNextSpawnTime();
    }

    private float CalcNextSpawnTime() {
        return Time.time + Random.Range(MinimumTime, MaximumTime);
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * If the prefab we spawned doesn't exist anymore (or was never
         * spawned), and we have waited a sufficient number of seconds, spawn
         * the prefab.
         */
        if (_spawnedObject == null) {
            if (Time.time >= _nextSpawnTime) {
                _nextSpawnTime = CalcNextSpawnTime();
                // Spawn
                _spawnedObject = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)]);
                _spawnedObject.transform.position = transform.position;
                _spawnedObject.transform.rotation = transform.rotation;

                // TODO add particles
            }
        } else {
            _nextSpawnTime = CalcNextSpawnTime();
        }
    }
}

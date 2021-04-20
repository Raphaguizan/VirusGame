using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnObject
{
    [SerializeField]
    public GameObject spawn;

    [SerializeField]
    [Tooltip("time of the prefab spawn")]
    public float spawnTime;
}

public class PerfabSpawner : MonoBehaviour
{
    [SerializeField]
    private SpawnObject[] spawnsTimes;

    private int spawnIndex = 0;

    private void Update()
    {
        if(spawnIndex >= spawnsTimes.Length) return;
        if(spawnsTimes[spawnIndex].spawnTime < Time.timeSinceLevelLoad){
            Instantiate(spawnsTimes[spawnIndex].spawn, transform.position, transform.rotation);
            spawnIndex++;
        }
    }
}

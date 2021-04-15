using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private int spawnCount = 0;
    [SerializeField]
    List<Wave> waves;

    public void Spawn()
    {
        if (spawnCount >= waves.Count)
        {
            Debug.Log("terminou as waves");
            return;
        }

        Wave currentWave = waves[spawnCount];
        foreach (SpawnActions singelEvent in currentWave.spawns)
        {
            if (singelEvent.index > transform.childCount) return;
            SpawnObject objAux = singelEvent.obj;
            GameObject aux = Instantiate(objAux.gameObject, transform.GetChild(singelEvent.index).position, this.transform.rotation);
            aux.GetComponent<Enemy>().Initialize(objAux.color, objAux.speed);
        }
        spawnCount++;
    }
}

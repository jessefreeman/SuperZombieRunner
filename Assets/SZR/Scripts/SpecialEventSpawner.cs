using jessefreeman.utools;
using UnityEngine;

public class SpecialEventSpawner : Spawner
{
    public int spawnCounter = 1;
    public int spawnNumberTrigger = 5;

    public GameObject[] specialPrefabs;

    protected override GameObject GetNextPrefab()
    {
        GameObject prefab = null;

        if (spawnCounter >= spawnNumberTrigger)
        {
            prefab = specialPrefabs[Random.Range(0, specialPrefabs.Length)];
            spawnCounter = 0;
        }
        else
        {
            prefab = base.GetNextPrefab();
        }

        spawnCounter++;

        return prefab;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyToSpawn;

    public Transform[] EnemySpawnpoints;

    BoxCollider trigger;
    private void Start()
    {
        trigger = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SpawnEnemies();
            trigger.enabled = false;
        }
    }

    void SpawnEnemies()
    {
        foreach (var sp in EnemySpawnpoints)
        {
            Instantiate(EnemyToSpawn,sp.position,sp.rotation);
            EnemyToSpawn.SetActive(true);
        }
    }
}

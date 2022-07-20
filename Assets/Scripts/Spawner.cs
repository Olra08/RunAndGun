using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject alien;
    public GameObject bird;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        Instantiate(alien, transform);
    }
}

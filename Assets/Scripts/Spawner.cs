using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawn;
    private float cooldownA;
    private float cooldownB;

    private void Update()
    {
        cooldownA -= Time.deltaTime;
        if (cooldownA <= 0f)
        {
            SpawnAliens();
        }
        cooldownB -= Time.deltaTime;
        if (cooldownB <= 0f)
        {
            SpawnBirds();
        }
    }

    private void SpawnAliens()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Alien"), spawn);
        obj.transform.parent = null;
        cooldownA = Random.Range(1f, 2.5f);
    }

    private void SpawnBirds()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Bird"), spawn);
        obj.transform.parent = null;
        cooldownB = Random.Range(5f, 8f);
    }

}

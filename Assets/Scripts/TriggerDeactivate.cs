using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDeactivate : MonoBehaviour
{
    private BoxCollider2D mCollider;
    public GameObject birds = null;
    public GameObject player = null;

    private void Start()
    {
        mCollider = transform.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mCollider.enabled = false;
        }
        if (collision.gameObject.CompareTag("LockR"))
        {
            if (gameObject.CompareTag("SpawnM"))
            {
                mCollider.enabled = false;
                GameObject obj = Instantiate(Resources.Load<GameObject>("CapsuleM"), transform);
                obj.transform.parent = null;
            }
            if (gameObject.CompareTag("SpawnL"))
            {
                mCollider.enabled = false;
                GameObject obj = Instantiate(Resources.Load<GameObject>("CapsuleL"), transform);
                obj.transform.parent = null;
            }
            if (gameObject.CompareTag("SpawnS"))
            {
                mCollider.enabled = false;
                GameObject obj = Instantiate(Resources.Load<GameObject>("CapsuleS"), transform);
                obj.transform.parent = null;
            }
            if (gameObject.CompareTag("SpawnB"))
            {
                mCollider.enabled = false;
                GameObject obj = Instantiate(Resources.Load<GameObject>("CapsuleB"), transform);
                obj.transform.parent = null;
            }
            if (gameObject.CompareTag("SpawnBird"))
            {
                birds.SetActive(true);
            }
            if (gameObject.CompareTag("Checkpoint1"))
            {
                Debug.Log("checkpoint1 set");
                PlayerPrefs.SetInt("checkpoint", 1);
            }
            if (gameObject.CompareTag("Checkpoint2"))
            {
                Debug.Log("checkpoint2 set");
                PlayerPrefs.SetInt("checkpoint", 2);
            }
        }
    }
}

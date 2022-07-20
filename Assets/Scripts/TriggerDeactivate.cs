using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDeactivate : MonoBehaviour
{
    private BoxCollider2D mCollider;

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
            if (gameObject.CompareTag("SpawnBird"))
            {
                mCollider.enabled = false;
                GameObject obj = Instantiate(Resources.Load<GameObject>("Bird"), transform);
                obj.transform.parent = null;
            }

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
        }
    }
}

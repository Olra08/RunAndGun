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
    }
}

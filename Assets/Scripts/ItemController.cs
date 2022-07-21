using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject explosion = null;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("IconB"))
            {
                StartCoroutine(DestroyBomb());
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator DestroyBomb()
    {
        Instantiate(explosion, transform);
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
    }
}

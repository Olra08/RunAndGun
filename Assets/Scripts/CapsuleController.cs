using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleController : MonoBehaviour
{
    public GameObject itemM;
    public GameObject itemL;
    public GameObject itemS;
    public GameObject itemB;
    private Vector3 position;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            position = new Vector3(transform.position.x, transform.position.y);
            if (gameObject.CompareTag("CapsuleM"))
            {
                
                itemM.transform.position = position;
                Instantiate(itemM, itemM.transform);
                Destroy(gameObject);
            }
            if (gameObject.CompareTag("CapsuleL"))
            {
                
                itemL.transform.position = position;
                Instantiate(itemL, itemL.transform);
                Destroy(gameObject);
            }
            if (gameObject.CompareTag("CapsuleS"))
            {
                
                itemS.transform.position = position;
                Instantiate(itemS, itemS.transform);
                Destroy(gameObject);
            }
            if (gameObject.CompareTag("CapsuleB"))
            {
                
                itemB.transform.position = position;
                Instantiate(itemB, itemB.transform);
                Destroy(gameObject);
            }
        }
    }
}

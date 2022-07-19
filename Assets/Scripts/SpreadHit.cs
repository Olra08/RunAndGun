using UnityEngine;

public class SpreadHit : MonoBehaviour
{
    public GameObject bullet;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hit"))
        {
            bullet.SetActive(false);
        }
    }
}

using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    private float mHealth = 100f;

    public GameObject explosion;
    private CinemachineImpulseSource impulse;
    public AudioSource mGameMusic;

    private void Start()
    {
        impulse = transform.GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        if (mHealth <= 0)
        {
            MultipleExplosions();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            mHealth--;

            if (mHealth == 0)
            {
                StartCoroutine(Dying());
                mGameMusic.clip = Resources.Load<AudioClip>("BossExplosion");
                mGameMusic.Play();
            }
        }
    }

    private void MultipleExplosions()
    {
        for (int i = 0; i < 5; i++)
        {
            float spawnY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            Instantiate(explosion, spawnPosition, Quaternion.identity);
        }
    }

    IEnumerator Dying()
    {
        impulse.GenerateImpulse(5f);
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
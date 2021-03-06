using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    private float mHealth = 100f;

    public GameObject explosionB;
    private CinemachineImpulseSource impulse;
    public AudioSource mGameMusic;
    public AudioSource mAudioSource;
    public Transform spawn1 = null;
    public Transform spawn2 = null;
    public GameObject alien;
    private float cooldown = 1f;
    bool dying = false;
    public Transform blackScreen;
    public Animator black;

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "HardScene")
        {
            mHealth = 500f;
        }
        impulse = transform.GetComponent<CinemachineImpulseSource>();
        spawn1 = transform.Find("Spawn1");
        spawn2 = transform.Find("Spawn2");
        mAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy && !dying)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0f)
            {
                SpawnEnemies();
            }
            
        }
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
            mAudioSource.PlayOneShot(Resources.Load<AudioClip>("damage_snes"));

            if (mHealth == 0)
            {
                StartCoroutine(Dying());
                StartCoroutine(FadeAudioSource.StartFade(mGameMusic, 0.1f, 1f));
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
            Instantiate(explosionB, spawnPosition, Quaternion.identity);
        }
    }

    IEnumerator Dying()
    {
        dying = true;
        impulse.GenerateImpulse(5f);
        yield return new WaitForSeconds(5);
        StartCoroutine(ChangeScreen());
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
        mGameMusic.Stop();
    }
    
    private void SpawnEnemies()
    {
        GameObject obj1 = Instantiate(alien, spawn1);
        //obj1.transform.parent = null;
        GameObject obj2 = Instantiate(alien, spawn2);
        //obj2.transform.parent = null;
        cooldown = 2f;
    }

    IEnumerator ChangeScreen()
    {
        blackScreen.gameObject.SetActive(true);
        black.SetBool("GameOver", true);
        yield return new WaitForSeconds(5);
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "HardScene")
        {
            SceneManager.LoadScene("WinHScene");
        }
        else
        {
            SceneManager.LoadScene("WinNScene");
        }
    }
}
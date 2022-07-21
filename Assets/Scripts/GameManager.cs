using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager mInstance;
    public AudioSource mGameMusic;

    public static GameManager GetInstance()
    {
        return mInstance;
    }

    public PlayerController player;
    public PlayerMovement movement;

    private void Awake()
    {
        mInstance = this;
        mGameMusic = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "HardScene")
        {
            mGameMusic.clip = Resources.Load<AudioClip>("StageHard");
            mGameMusic.Play();
        }
        else
        {
            mGameMusic.clip = Resources.Load<AudioClip>("StageNormal");
            mGameMusic.Play();
        }
    }
}
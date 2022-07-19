using System;
using UnityEngine;

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
        mGameMusic.clip = Resources.Load<AudioClip>("StageNormal");
        mGameMusic.Play();
    }
}
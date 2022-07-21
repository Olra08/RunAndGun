using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    private AudioSource mAudioSource;

    private void Awake()
    {
        PlayerPrefs.SetInt("checkpoint", 0);
    }

    private void Start()
    {
        mAudioSource = transform.GetComponent<AudioSource>();
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "WinHScene")
        {
            mAudioSource.PlayOneShot(Resources.Load<AudioClip>("FinalVictory"));
        }
        else
        {
            mAudioSource.PlayOneShot(Resources.Load<AudioClip>("StageClear"));
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene("HardScene");
        }
    }
}

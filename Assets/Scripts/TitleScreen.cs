using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("checkpoint", 0);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}

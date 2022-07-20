using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}

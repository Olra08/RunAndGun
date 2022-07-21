using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "LoseHScene")
            {
                SceneManager.LoadScene("HardScene");
            }
            else
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }
}

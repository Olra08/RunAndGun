using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager mInstance;

    public static GameManager GetInstance()
    {
        return mInstance;
    }

    public PlayerController player;
    public PlayerMovement movement;

    private void Awake()
    {
        mInstance = this;
    }
    private void Start()
    {

    }
}
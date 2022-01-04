using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static int health;
    public static string color = "Gray";
    public static string difficulty = "Normal";
    [HideInInspector] public static float numCollectibles = 0f;
    [HideInInspector] public static float score = 0f;
    [HideInInspector] public static string playerName = "Toaster";

    public static PlayerManager Instance { get; private set;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        enabled = false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset player info if player restarts from the main menu.
        if (scene.buildIndex == 0)
        {
            color = "Gray";
            difficulty = "Normal";
            health = 10;
            numCollectibles = 0f;
            score = 0f;
            playerName = "Toaster";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGManager : MonoBehaviour
{
    [SerializeField] public AudioSource bgmOne;
    [SerializeField] public AudioSource bgmTwo;
    [SerializeField] private AudioSource bgmMenu;
    
    public static BGManager Instance { get; private set; }

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

    private void Start()
    {
        bgmMenu.Play();    
    }

    private void Update()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        enabled = false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0 || scene.buildIndex == 1 || scene.buildIndex == 4 || scene.buildIndex == 6)
        {
            if (!bgmMenu.isPlaying)
            {
                bgmMenu.Play();
            }
            else
            {
                return;
            }
        }
        else
        {
            bgmMenu.Stop();
        }
    }
}

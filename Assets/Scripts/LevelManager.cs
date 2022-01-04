using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private AudioManager levelAudio;
    private BGManager bgmManager;

    public static LevelManager Instance { get; private set;}

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
        levelAudio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        bgmManager = GameObject.Find("BGManager").GetComponent<BGManager>();
    }

    private void Update()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void CompletedLevel()
    {
        bgmManager.bgmOne.Stop();
        bgmManager.bgmTwo.Stop();
        levelAudio.levelCompleted.Play();
        Invoke("GoNextLevel", 9.0f);
    }

    public void FailedLevel()
    {
        bgmManager.bgmTwo.Stop();
        bgmManager.bgmOne.Stop();
    }

    private void GoNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(5);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Play different level music depending on level.
        if(scene.buildIndex == 2)
        {
            bgmManager.bgmOne.Play();
        }
        else if (scene.buildIndex == 3)
        {
            bgmManager.bgmTwo.Play();
        }
    }

}

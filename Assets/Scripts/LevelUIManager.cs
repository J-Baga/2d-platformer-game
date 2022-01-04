using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField] private Text healthText;
    [SerializeField] private Text collectibleText;
    [SerializeField] private Text nameText;
    [SerializeField] private Text scoreText;

    public static LevelUIManager Instance { get; private set;}
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
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

    private void Start()
    {
        healthText.text = PlayerManager.health.ToString();
        collectibleText.text = PlayerManager.numCollectibles.ToString();
        nameText.text = PlayerManager.playerName;
        scoreText.text = PlayerManager.score.ToString();
        collectibleUpdate();
        healthUpdate();
        scoreUpdate();
        nameUpdate();
    }

    public void collectibleUpdate()
    {
        collectibleText.text = PlayerManager.numCollectibles.ToString();
    }

    public void healthUpdate()
    {
        healthText.text = PlayerManager.health.ToString();
    }

    public void scoreUpdate()
    {
        scoreText.text = PlayerManager.score.ToString();
    }

    public void nameUpdate()
    {
        nameText.text = PlayerManager.playerName;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Level UI only loads on gameplay scenes.
        if (scene.buildIndex == 2 || scene.buildIndex == 3)
        {
            gameObject.SetActive(true);
            healthText.text = PlayerManager.health.ToString();
            collectibleText.text = PlayerManager.numCollectibles.ToString();
            nameText.text = PlayerManager.playerName;
            scoreText.text = PlayerManager.score.ToString();
            collectibleUpdate();
            healthUpdate();
            scoreUpdate();
            nameUpdate();
        }
    }
}

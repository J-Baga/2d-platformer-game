using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private InputField charName;
    private Dropdown ddColor, ddDifficulty;
    private Text difficultyDescription, playerName, playerScore, playerCollectibles;
    private Button buttonStartGame, buttonMenu, buttonPlayGame, buttonQuitGame, buttonTutorial;
    private SpriteRenderer playerAppearance;

    public static UIManager Instance { get; private set; }

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
        buttonPlayGame = GameObject.Find("Button_Play").GetComponent<Button>();
        buttonPlayGame.onClick.AddListener(PlayGame);

        buttonQuitGame = GameObject.Find("Button_Quit").GetComponent<Button>();
        buttonQuitGame.onClick.AddListener(ExitGame);

        buttonTutorial = GameObject.Find("Button_How_To_Play").GetComponent<Button>();
        buttonTutorial.onClick.AddListener(Tutorial);
    }

    private void Update()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        enabled = false;
    }

    private void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void SetCharName(string name)
    {
        PlayerManager.playerName = name;
    }

    private void SetColor(string color)
    {
        PlayerManager.color = color;
        switch (color)
        {
            case "Gray":
                playerAppearance.color = Color.white;
                break;

            case "Red":
                playerAppearance.color = Color.red;
                break;

            case "Blue":
                playerAppearance.color = Color.blue;
                break;

            case "Green":
                playerAppearance.color = Color.green;
                break;

            case "Magenta":
                playerAppearance.color = Color.magenta;
                break;

            case "Cyan":
                playerAppearance.color = Color.cyan;
                break;

            case "Yellow":
                playerAppearance.color = Color.yellow;
                break;

            case "Black":
                playerAppearance.color = Color.black;
                break;
        }
    }

    private void SetDifficulty(string difficulty)
    {
        PlayerManager.difficulty = difficulty;
        switch (difficulty)
        {
            case "Normal":
                difficultyDescription.text = "A difficulty for players of all skills and ages! You will be given 20 Health at the start " +
                    "of each level.";
                break;

            case "Easy":
                difficultyDescription.text = "A difficulty for beginners. You will be given 30 health at the start " +
                    "of each level.";
                break;

            case "Hard":
                difficultyDescription.text = "A difficulty for more experienced players. You will be given 10 health at the start " +
                    "of each level.";
                break;

            case "Expert":
                difficultyDescription.text = "A difficulty for hardcore players looking for a challenge. You will be given 5 health at the " +
                    "start of each level.";
                break;

            case "Chaotic":
                difficultyDescription.text = "A difficulty for players looking to ascend humanity. You will be given 1 health at the " +
                    "start of each level.";
                break;
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    private void Tutorial()
    {
        SceneManager.LoadScene(6);
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 0)
        {
            buttonPlayGame = GameObject.Find("Button_Play").GetComponent<Button>();
            buttonPlayGame.onClick.AddListener(PlayGame);

            buttonQuitGame = GameObject.Find("Button_Quit").GetComponent<Button>();
            buttonQuitGame.onClick.AddListener(ExitGame);

            buttonTutorial = GameObject.Find("Button_How_To_Play").GetComponent<Button>();
            buttonTutorial.onClick.AddListener(Tutorial);
        }
        else if(scene.buildIndex == 1)
        {
            charName = GameObject.Find("InputField_Name").GetComponent<InputField>();
            charName.onEndEdit.AddListener(delegate { SetCharName(charName.text.ToString()); });

            ddColor = GameObject.Find("Dropdown_Color").GetComponent<Dropdown>();
            ddColor.onValueChanged.AddListener(delegate { SetColor(ddColor.options[ddColor.value].text.ToString()); });

            ddDifficulty = GameObject.Find("Dropdown_Difficulty").GetComponent<Dropdown>();
            ddDifficulty.onValueChanged.AddListener(delegate { SetDifficulty(ddDifficulty.options[ddDifficulty.value].text.ToString()); });

            difficultyDescription = GameObject.Find("Text_Difficulty_Description").GetComponent<Text>();

            buttonStartGame = GameObject.Find("Button_Start_Game").GetComponent<Button>();
            buttonStartGame.onClick.AddListener(StartGame);

            buttonMenu = GameObject.Find("Button_Main_Menu").GetComponent<Button>();
            buttonMenu.onClick.AddListener(MainMenu);

            playerAppearance = GameObject.Find("PlayerAppearance").GetComponent<SpriteRenderer>();
        }
        else if (scene.buildIndex == 4)
        {
            playerScore = GameObject.Find("Text_Final_Score").GetComponent<Text>();
            playerScore.text = PlayerManager.score.ToString();

            playerCollectibles = GameObject.Find("Text_Final_Avocado").GetComponent<Text>();
            playerCollectibles.text = PlayerManager.numCollectibles.ToString();

            buttonMenu = GameObject.Find("Button_Main_Menu").GetComponent<Button>();
            buttonMenu.onClick.AddListener(MainMenu);

            buttonQuitGame = GameObject.Find("Button_Quit").GetComponent<Button>();
            buttonQuitGame.onClick.AddListener(ExitGame);

            playerAppearance = GameObject.Find("PlayerAppearance").GetComponent<SpriteRenderer>();
            SetColor(PlayerManager.color);
        }
        else if (scene.buildIndex == 5)
        {
            playerName = GameObject.Find("Text_Name").GetComponent<Text>();
            playerScore = GameObject.Find("Text_Score").GetComponent<Text>();

            buttonMenu = GameObject.Find("Button_Main_Menu").GetComponent<Button>();
            buttonMenu.onClick.AddListener(MainMenu);

            buttonQuitGame = GameObject.Find("Button_Quit").GetComponent<Button>();
            buttonQuitGame.onClick.AddListener(ExitGame);

            playerName.text = PlayerManager.playerName;
            playerScore.text = PlayerManager.score.ToString();
        }
        else if (scene.buildIndex == 6)
        {
            buttonMenu = GameObject.Find("Button_Main_Menu").GetComponent<Button>();
            buttonMenu.onClick.AddListener(MainMenu);
        }
    }
}

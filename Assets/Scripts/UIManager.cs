using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private VisualElement MainPanel;
    private VisualElement MainMenuPanel;
    private VisualElement GameOverPanel;
    private Button startButton;
    private Button exitButton;
    private Button restartButton;
    private Button mainMenuButton;
    private VisualElement ScoreTable;
    private VisualElement ScorePanel;
    private VisualElement HighScorePanel;
    public Label Score;
    private Label HighScore;
    private int currentScore = 0;
    private int scoreValue = 0;
    public bool isGameOver = true;
    private float accumulatedScore = 0f;

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
    public void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        MainPanel = root.Q<VisualElement>("MainPanel");
        MainMenuPanel = root.Q<VisualElement>("MainMenuPanel");
        GameOverPanel = root.Q<VisualElement>("GameOverPanel");
        startButton = root.Q<Button>("startButton");
        exitButton = root.Q<Button>("exitButton");
        restartButton = root.Q<Button>("restartButton");
        mainMenuButton = root.Q<Button>("mainMenuButton");
        ScoreTable = root.Q<VisualElement>("ScoreTable");
        ScorePanel = root.Q<VisualElement>("ScorePanel");
        HighScorePanel = root.Q<VisualElement>("HighScorePanel");
        Score = root.Q<Label>("Score");
        HighScore = root.Q<Label>("HighScore");
        startButton.clicked += StartGame;
        exitButton.clicked += ExitGame;
        restartButton.clicked += RestartGame;
        mainMenuButton.clicked += ShowMainMenu;
        Time.timeScale = 0f;
        int savedHighScore = PlayerPrefs.GetInt("HighScore",0);
        HighScore.text = savedHighScore.ToString();
        
        UpdateScore();
    }

    private void Update()
    {
        if (isGameOver != true)
        {
            accumulatedScore += Time.deltaTime * 10f;
            int pointsToAdd = Mathf.FloorToInt(accumulatedScore);
            if (pointsToAdd > 0)
            {
                AddScore(pointsToAdd);
                accumulatedScore -= pointsToAdd;
            }
        }
    }

    public void AddScore(int amount)
    {
        scoreValue += amount;
        UpdateScore();
    }
    public void ScoreReset()
    {
        scoreValue = 0;
        UpdateScore();
    }
    private void UpdateScore()
    {
        Score.text = scoreValue.ToString();
    }
    public void GameOverScreen()
    {
        GameOverPanel.style.display = DisplayStyle.Flex;
        MainMenuPanel.style.display = DisplayStyle.None;
        
        currentScore = scoreValue;
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (currentScore > savedHighScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);  // Yeni high score'u kaydet
            PlayerPrefs.Save();  // Veriyi hemen diske yaz
        }

        HighScore.text = PlayerPrefs.GetInt("HighScore").ToString();  // Güncel değeri label’a yazdır

        
        ScoreReset();
        isGameOver = true;
    }
    private void RestartGame()
    {
        isGameOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameOverPanel.style.display = DisplayStyle.None;
        
    }

    private void StartGame()
    {
        isGameOver = false;
        GameOverPanel.style.display = DisplayStyle.None;
        MainMenuPanel.style.display = DisplayStyle.None;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ExitGame()
    {
        isGameOver = true;
        ScoreReset();
        Debug.Log("Oyun kapatılıyor...");
        Application.Quit();
    }

    private void ShowMainMenu()
    {
        MainMenuPanel.style.display = DisplayStyle.Flex;
        GameOverPanel.style.display = DisplayStyle.None;
    }
    
}
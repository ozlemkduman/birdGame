using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
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

    

    public void GameOver()
    {
        Debug.Log("Game Over!");
        if (UIManager.Instance != null)
        {
            UIManager.Instance.GameOverScreen();
        }
        else
        {
            Debug.LogError("UIManager Instance null! Sahnede UIManager yok veya yanlış bağlandı.");
        }

        Time.timeScale = 0f; 
    }
}
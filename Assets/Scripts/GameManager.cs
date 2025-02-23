using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioSource MainMenuAudioSource;
    public AudioSource BackGroundAudioSource;
    public AudioSource GameOverAudioSource;
    
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
        if (MainMenuAudioSource != null) DontDestroyOnLoad(MainMenuAudioSource.gameObject);
        if (BackGroundAudioSource != null) DontDestroyOnLoad(BackGroundAudioSource.gameObject);
        if (GameOverAudioSource != null) DontDestroyOnLoad(GameOverAudioSource.gameObject);

        PlayMainMenuAudio();
    }

    public void PlayMainMenuAudio()
    {
        if (MainMenuAudioSource != null && !MainMenuAudioSource.isPlaying)
        {
            MainMenuAudioSource.Play();
        }
    }
    private void StopMainMenuAudio()
    {
        if (MainMenuAudioSource != null )
        {
            MainMenuAudioSource.Stop();
        }
    }
    public void PlayBackGroundAudio()
    {
        if (BackGroundAudioSource != null && !BackGroundAudioSource.isPlaying)
        {   
            StopMainMenuAudio();
            BackGroundAudioSource.loop = true;
            BackGroundAudioSource.Play();
        }
    }

    private void StopBackGroundAudio()
    {
        if (BackGroundAudioSource != null)
        {
            BackGroundAudioSource.Stop();
        }   
    }

    public void GameOverAudio()
    {
        StopBackGroundAudio();
        if (GameOverAudioSource != null)
        {
            GameOverAudioSource.Play();
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
using UnityEngine;
using System.Collections;
public class BirdScripts : MonoBehaviour
{
    public float jumpForce = 6f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.linearVelocity = Vector2.up * jumpForce;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitObject = collision.gameObject;

        if (hitObject.CompareTag("Barrier") || hitObject.CompareTag("Ground"))
        {
            if (GameManager.Instance != null)
            {
                Debug.Log(hitObject.name);
                GameManager.Instance.GameOverAudio();
                StartCoroutine(DelayedGameOver());  // 1 saniye bekleyen coroutine başlatılıyor
            }
            else
            {
                Debug.LogError("GameManager Instance null! Sahnede GameManager yok.");
            }
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator DelayedGameOver()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1f); // Gerçek zamanlı 1 saniye bekle
        Time.timeScale = 1f;
        GameManager.Instance.GameOver();  // Sonra GameOver çağır
    }
    
}
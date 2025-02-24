using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BirdScripts : MonoBehaviour
{
    public static BirdScripts instance;
    public float jumpForce = 6f;
    private Rigidbody2D rb;
    public int HealthBird = 100;
    private Animator _anim;

    void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (_anim != null)
        {
            _anim.Play("Bird1_1"); // "Fly" animasyonun adını kullan
        }
        else
        {
            Debug.LogError("Animator bileşeni bulunamadı!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.linearVelocity = Vector2.up * jumpForce;
        }
       
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Lightning"))
        {
            Debug.Log(other.gameObject.name+ "simseke dokundu");
            ReduceHealth();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitObject = collision.gameObject;
        
        if (hitObject.CompareTag("Barrier") || hitObject.CompareTag("Ground")||HealthBird == 0)
        {
            if (GameManager.Instance != null)
            {
                Debug.Log(hitObject.name);
                GameManager.Instance.GameOverAudio();
                StartCoroutine(DelayedGameOver());
            }
            else
            {
                Debug.LogError("GameManager Instance null! Sahnede GameManager yok.");
            }
        }
    }

    void ReduceHealth()
    {
        HealthBird -= 10;
        
    }
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator DelayedGameOver()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1.5f); 
        Time.timeScale = 1f;
        GameManager.Instance.GameOver(); 
    }
    
}
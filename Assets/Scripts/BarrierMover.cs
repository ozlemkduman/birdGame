using System;
using UnityEngine;

public class BarrierMover : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 3f;
    private float currentSpeed;
    private float leftBound = -17f;

    private void Start()
    {
        currentSpeed = initialSpeed;
    }

    void Update()
    {
        if (!UIManager.Instance.isGameOver)
        {
            // SpawnManager'daki globalSpeed'i kullan
            transform.Translate(Vector3.left * (SpawnManager.instance.globalSpeed * Time.deltaTime));

            if (transform.position.x < leftBound)
            {
                gameObject.SetActive(false);
            }
        }
    }
}


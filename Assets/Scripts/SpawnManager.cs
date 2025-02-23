using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    
    [SerializeField] private GameObject barrierPrefab;
    private int poolSize = 10; // Havuzda kaç engel olacak

    private float minSpawnTime = 2f;
    private float maxSpawnTime = 4f;
    private float minXDistance = 2f; // Minimum X mesafesi
    private float minYDistance = 2f; // Minimum Y mesafesi
    [SerializeField] private Transform spawnPoint;

    private float spawnTimer;
    private Vector2 lastSpawnPosition = new Vector2(99, 99);

    private List<GameObject> barrierPool;

    // Engel hızı yönetimi
    public float globalSpeed = 3f; // Başlangıç hızı
    public float speedIncreaseRate = 0.03f; // Hız artış miktarı
    private int lastCheckedScore = -1; // Hangi puanda hız arttı

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        // Engel havuzunu oluştur
        barrierPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject barrier = Instantiate(barrierPrefab);
            barrier.SetActive(false);
            barrierPool.Add(barrier);
        }

        spawnTimer = 1f;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnPipePair();
            spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
        }

        int currentScore = int.Parse(UIManager.Instance.Score.text);
        
        // Skor 250'nin katlarına ulaştığında hız artır
        if (currentScore % 250 == 0 && currentScore != lastCheckedScore)
        {
            globalSpeed += globalSpeed * speedIncreaseRate;
            lastCheckedScore = currentScore;
            Debug.Log("Yeni hız: " + globalSpeed);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void SpawnPipePair()
    {
        float posY;
        float posX;
        int attempts = 0;
        
        do
        {
            posY = Random.Range(-5f, 4f);
            posX = 16f;

            attempts++;
            if (attempts > 10) break; // Sonsuz döngüyü önlemek için
        } while ((Mathf.Abs(posY - lastSpawnPosition.y) < minYDistance) || 
                 (Mathf.Abs(posX - lastSpawnPosition.x) < minXDistance));

        GameObject newPipe = GetPooledBarrier();
        if (newPipe)
        {
            newPipe.transform.position = new Vector3(posX, posY, 0f);
            newPipe.SetActive(true);

            // 🔥 Animasyonu başlat
            Animator anim = newPipe.GetComponent<Animator>();
            if (anim != null)
            {
                anim.Play("EnemyIdle");  // 🎬 Animasyon adını kontrol et
            }
            lastSpawnPosition = new Vector2(posX, posY); // Yeni konumu güncelle
        }

    }

    GameObject GetPooledBarrier()
    {
        foreach (GameObject barrier in barrierPool)
        {
            if (!barrier.activeInHierarchy)
            {
                barrier.SetActive(true);
                return barrier;
            }
        }

        // Eğer kullanılmayan engel yoksa, yeni oluştur ve havuza ekle
        GameObject newBarrier = Instantiate(barrierPrefab);
        newBarrier.SetActive(true);
        barrierPool.Add(newBarrier);

        return newBarrier;
    }
}

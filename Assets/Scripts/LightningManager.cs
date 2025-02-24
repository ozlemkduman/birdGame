using System.Collections;
using UnityEngine;

public class LightningManager : MonoBehaviour
{
    public static LightningManager instance;
    public GameObject Lightning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Lightning != null)
        {
            Lightning.SetActive(false);
            StartCoroutine(LightningRoutine());
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator LightningRoutine()
    {
        while (true) // Sonsuz döngü içinde rastgele zamanlarda çalışacak
        {
            // Rastgele bir süre bekle (örneğin 3 ile 7 saniye arasında)
            float randomWaitTime = Random.Range(1f, 7f);
            yield return new WaitForSeconds(randomWaitTime);

            // Şimşeği aç
            if (Lightning != null)
            {
                Lightning.SetActive(true);
                Debug.Log("Şimşek Çaktı!");

                // Şimşeğin ne kadar süre açık kalacağını belirle (örneğin 0.1 ile 0.5 saniye)
                 float flashDuration = 10f;
                yield return new WaitForSeconds(flashDuration);

                // Şimşeği kapat
                Lightning.SetActive(false);
                Debug.Log("Şimşek Söndü!");
            }
        }
    }

    
    
    
}

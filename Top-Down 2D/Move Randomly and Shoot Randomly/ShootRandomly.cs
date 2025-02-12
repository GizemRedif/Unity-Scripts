using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRandomly : MonoBehaviour
{
    public GameObject thrownObjectPrefab; 
    public float objectThrowInterval = 2f; // Obje fırlatma aralığı
    public float objectSpawnDistance = 1f; 

    private float timeSinceLastThrownObject = 0f;

    void Start()
    {
    }

    void Update()
    {
        ThrowObjectRandomly();
    }


    void ThrowObjectRandomly()
    {
        timeSinceLastThrownObject += Time.deltaTime;

        if (timeSinceLastThrownObject >= objectThrowInterval)
        {
            timeSinceLastThrownObject = 0f;

            // Rastgele bir yön hesapla
            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

            // Objenin karakterden rastgele bir pozisyonda oluşturulması(eğer bunu yapmazsam obje hep aynı taraftan fırlatılıyor)
            Vector2 spawnPosition = (Vector2)transform.position + randomDirection * objectSpawnDistance;

            // objeyi oluştur
            GameObject thrownObject = Instantiate(thrownObjectPrefab, spawnPosition, Quaternion.identity);

            // objeye hareket yönünü ata
            RandomlyThrownObject thrownObjectScript = thrownObject.GetComponent<RandomlyThrownObject>();
            if (thrownObjectScript != null)
            {
                thrownObjectScript.SetDirection(randomDirection); //RandomlyThrownObject scriptine ulaşıyoruz
            }
            else
            {
                Debug.LogError("ThrownObject prefab'ında 'RandomlyThrownObject' script'i bulunamadı!");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandomly : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeDirectionTime = 2f; // Yön değiştirme süresi (karakter rastgele hareket edeceği için)

    private float timeSinceLastDirectionChange = 0f;
    private Vector2 moveDirection;

    void Start()
    {
        // Başlangıçta rastgele bir yön belirle
        ChooseNewDirection();
    }

    void Update()
    {
        MoveInCurrentDirection(); // Karakteri hareket ettir
    }

    void MoveInCurrentDirection()
    {
        timeSinceLastDirectionChange += Time.deltaTime;

        // Belirli bir süre geçince yönü değiştirmesi için
        if (timeSinceLastDirectionChange >= changeDirectionTime)
        {
            ChooseNewDirection();
        }

        // Seçilen yönde karakteri hareket ettir
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    // Rastgele yeni bir yön seçer
    void ChooseNewDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        // Yönün doğrultusunu normalleştir (hızı dengeli olsun diye)
        moveDirection = new Vector2(randomX, randomY).normalized;

        // Yön değiştirildikten sonra sıfırla
        timeSinceLastDirectionChange = 0f;
    }

    // Çarpışma olduğunda yön değiştir
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChooseNewDirection(); // Yeni rastgele bir yön seç
    }

    // Eğer obje trigger ise bunu kullan (örneğin: duvarlar "Is Trigger" olarak ayarlıysa)
    private void OnTriggerEnter2D(Collider2D other)
    {
        ChooseNewDirection(); // Yeni rastgele bir yön seç
    }
}

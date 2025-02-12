using System.Collections;
using UnityEngine;

public class FollowingCharacter : MonoBehaviour
{
    public float speed = 2f;
    public float followDistance = 5f;
    public Transform hero;
    public GameObject thrownObjectPrefab;
    public float thrownObjectSpeed = 4f; 
    public float thrownObjectInterval = 2f;

    private Vector2 randomDirection;
    private bool isFollowing = false;
    private float nextShootTime = 0f;

    void Start()
    {
        // İlk başta rastgele bir yön belirle
        SetRandomDirection();
    }

    void Update()
    {
        float distanceToHero = Vector2.Distance(transform.position, hero.position);

        if (distanceToHero <= followDistance)
        {
            // Kahraman belli bir mesafeye gelirse kilitlenip takip etmeye başla
            isFollowing = true;
        }

        if (isFollowing)
        {
            FollowHero(distanceToHero);
            Shoot();
        }
        else
        {
            RandomMovement();
        }
    }

    void SetRandomDirection()
    {
        // Rastgele bir yön seç
        randomDirection = Random.insideUnitCircle.normalized;
    }

    void RandomMovement()
    {
        // Rastgele hareket et
        transform.Translate(randomDirection * speed * Time.deltaTime);

        // Rastgele yönü tekrar belirli aralıklarla değiştir
        if (Random.Range(0f, 1f) < 0.02f)
        {
            SetRandomDirection();
        }
    }

    void FollowHero(float distanceToHero)
    {
        // Kahramanı takip et ama her zaman belirlenen mesafeyi bırak
        if (distanceToHero > followDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, hero.position, speed * Time.deltaTime);
        }
    }

    void Shoot()
    {
        if (Time.time >= nextShootTime)
        {
            // Objeyi fırlat
            GameObject thrownObject = Instantiate(thrownObjectPrefab, transform.position, Quaternion.identity);
            Rigidbody2D thrownObjectRb = thrownObject.GetComponent<Rigidbody2D>();
            

            // Kahramana doğru bir yön belirle ve o yöne kuvvet uygulayarak ateş et
            Vector2 direction = (hero.position - transform.position).normalized;
            thrownObjectRb.AddForce(direction * thrownObjectSpeed, ForceMode2D.Impulse);

            nextShootTime = Time.time + thrownObjectInterval;
        }
    }
}
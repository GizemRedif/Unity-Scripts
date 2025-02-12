using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    public float speed = 2f;
    public float followDistance = 5f;
    public Transform hero;

    private Vector2 randomDirection;
    public bool isFollowing = false;
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
        // Kahramanı takip et ama her zaman 10f mesafe bırak
        if (distanceToHero > followDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, hero.position, speed * Time.deltaTime);
        }
    }
}

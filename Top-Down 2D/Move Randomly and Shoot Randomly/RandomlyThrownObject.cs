using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomlyThrownObject : MonoBehaviour
{
    public float speed = 5f; // obje fırlatma hızı
    public int damage = 10; // Kahramana vereceği hasar
    public float lifetime = 2f;

    private Vector2 direction;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            CharacterHealth characterHealthScript = collision.gameObject.GetComponent<CharacterHealth>();
            if (characterHealthScript != null)
            {
                characterHealthScript.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}

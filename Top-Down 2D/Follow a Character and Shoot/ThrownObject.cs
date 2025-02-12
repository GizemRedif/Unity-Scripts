using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObject : MonoBehaviour {
    
    public float lifetime = 5f;
    public int damage = 10; // Objenin kahramana vereceği zarar miktarı

    void Start()
    {
        // Belirli bir süre sonra obje yok olsun
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Kahramana çarptığında obje yok olsun ve kahraman hasar alsın
        if (collision.gameObject.CompareTag("Hero"))
        {
            // Kahramanın Health scriptini al
            CharacterHealth heroHealth = collision.gameObject.GetComponent<CharacterHealth>();

            // Eğer HeroHealth bileşeni varsa canını azalt
            if (heroHealth != null)
            {
                heroHealth.TakeDamage(damage);
            }
        }
        // Obje yok olsun
        Destroy(gameObject);
    }
}

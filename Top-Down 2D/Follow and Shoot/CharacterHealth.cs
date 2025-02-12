using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CharacterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthBar; // Can barı referansı

    void Start()
    {
        currentHealth = maxHealth; // Başlangıçta canı maksimum yap
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Remaining health of the character: " + currentHealth);

        UpdateHealthBar(); // Can barını güncelle

        // Eğer can 0 veya altına düşerse, karakter ölür
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth; // Can oranını güncelle
        }
    }

    void Die()
    {
        Debug.Log("Character is dead!");
        gameObject.SetActive(false); // Geçici olarak karakteri yok etmek için
    }
}

using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class KBMControls : MonoBehaviour
{
    private Rigidbody2D rb;  //İlgili karakterin rigidbody'ine ulaşılır
    Animator playerAnimator; //Karakterin ilgili hareketlerine animasyon eklemek icin kullanılacak
    public float moveSpeed = 1f; //Karakterin hızı
    bool facingRight = true; //Karakterin, yönünü yatayda hareket yönüne dönmesi için kullanılacak

    public GameObject thrownObjectPrefab; //Karakterin düşmana fırlatacağı obje
    public float thrownObjectSpeed = 20f; 
    public Transform firePoint; //Ateş edilecek nokta (Kullanıcı mause ile belirleyecek)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerMoveAnimation(); //Karakter WASD tuşlarıyla yönlendirilirken oynatılacak animasyonların fonksiyonu
    
        if(Input.GetMouseButtonDown(0))
        {
            Shoot(); //Kullanıcının mouse'in sol tuşuna bastığında ateş etmesini sağlayacak fonksiyon
            PlayerShootAnimation();
        }

        //Karakterin yüzünü yatayda gittiği yöne çeviren kodlar ve fonksiyon
        if(rb.velocity.x < 0 && facingRight)
        {
            FlipFace();
        }
        else if(rb.velocity.x > 0 && !facingRight)
        {
            FlipFace();
        }
    }

    void PlayerMoveAnimation()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal")*moveSpeed,Input.GetAxis("Vertical")*moveSpeed); //Karakterin yatay ve dikey hızını alır
            
        playerAnimator.SetFloat("horizontalSpeed", Math.Abs(rb.velocity.x)); //Karakterin yatayda yürüme animasyonu için kullanılır
        // playerAnimator.SetFloat("verticalSpeed",Math.Abs(rb.velocity.y)); Karakterin dikeyde de animasyonu varsa kullanılır
    }

   void PlayerShootAnimation()
    {
        playerAnimator.SetBool("isMouseButtonDown0", true);
        StartCoroutine(ResetShootAnimation()); // Coroutine başlat
    }

    IEnumerator ResetShootAnimation()
    {
        yield return new WaitForSeconds(0.5f); // 0.5 saniye bekle
        playerAnimator.SetBool("isMouseButtonDown0", false);
    }
    
    void FlipFace()
    {
        facingRight = !facingRight;
        Vector3 tempLocalScale = transform.localScale;
        tempLocalScale.x *= -1;
        transform.localScale = tempLocalScale;
    }

    void Shoot()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 firePointPosition = firePoint.position;
        Vector2 direction = (mousePosition - firePointPosition).normalized;

        GameObject thrownObject = Instantiate(thrownObjectPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = thrownObject.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = direction * thrownObjectSpeed;
        }

        // Objenin yönünü döndürme
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        thrownObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}

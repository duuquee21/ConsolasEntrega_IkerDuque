using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifetime = 3f;
    

    void Start()
    {
       
        Destroy(gameObject, lifetime);
        
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            
            if (GameManager.instance != null)
            {
                GameManager.instance.EnemyDefeated();
            }
            
            Destroy(collision.gameObject); // Destruye al enemigo
            Destroy(gameObject);           // Destruye la bala
        }
    }
}
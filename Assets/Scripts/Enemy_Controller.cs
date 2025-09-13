using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    private Transform player;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    public void SetEnemyType(string type)
    {
        
        if (spriteRenderer == null) return;
        switch (type)
        {
            case "Type1":
                spriteRenderer.color = new Color(1f, 0.5f, 0.5f); // Rojo claro
                break;
            case "Type2":
                spriteRenderer.color = new Color(0.5f, 0.8f, 1f); // Azul claro
                break;
            case "Type3":
                spriteRenderer.color = new Color(0.5f, 1f, 0.5f); // Verde claro
                break;
            case "Type4":
                spriteRenderer.color = new Color(1f, 1f, 0.5f); // Amarillo claro
                break;
            default:
                spriteRenderer.color = Color.white;
                break;
        }
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            // buscar script HealthManager en el jugador
            HealthManager playerHealth = collision.gameObject.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                // llamar a la función para hacerle daño al jugador
                playerHealth.TakeDamage(1);
            }

            
            if (GameManager.instance != null)
            {
                GameManager.instance.EnemyDefeated(); // avisar GameManager que este enemigo fue derrotado (al chocar)
            }
            
            
            Destroy(gameObject);
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement; 

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("¡El jugador ha sido derrotado!");
        // Carga la escena de derrota
        SceneManager.LoadScene("DefeatScene");
    }
}
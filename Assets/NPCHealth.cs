using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private bool isDead = false;
    public bool IsDead => isDead;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} hasar aldı! Kalan can: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} öldü!");
        // Burada destroy yerine animasyon veya ragdoll da yapabilirsin
        Destroy(gameObject);
    }
}
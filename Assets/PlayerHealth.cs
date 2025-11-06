using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    public TextMeshProUGUI healthText;

    [Header("Damage Settings")]
    public float damagePerNPC = 5f;    // Her NPC’nin vereceği hasar
    public float damageInterval = 1f;  // Kaç saniyede bir hasar alsın
    private float damageTimer = 0f;

    private List<GameObject> touchingNPCs = new List<GameObject>();

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        damageTimer += Time.deltaTime;

        // 🔹 Her belirli sürede, yaşayan NPC’lerden gelen toplam hasarı uygula
        if (damageTimer >= damageInterval && touchingNPCs.Count > 0)
        {
            int aliveNPCs = 0;

            foreach (GameObject npc in touchingNPCs)
            {
                if (npc == null) continue;

                var npcHealth = npc.GetComponent<NPCHealth>();
                if (npcHealth == null || npcHealth.IsDead) continue;

                aliveNPCs++;
            }

            if (aliveNPCs > 0)
                TakeDamage(aliveNPCs * damagePerNPC);

            damageTimer = 0f;
        }

        // 🔹 Listeden ölü ya da silinen NPC’leri çıkar
        touchingNPCs.RemoveAll(npc =>
        {
            if (npc == null) return true;
            var npcHealth = npc.GetComponent<NPCHealth>();
            return npcHealth == null || npcHealth.IsDead;
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC") && !touchingNPCs.Contains(other.gameObject))
        {
            touchingNPCs.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            touchingNPCs.Remove(other.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("💀 Player öldü!");
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = "Health: " + Mathf.RoundToInt(currentHealth);
    }
}
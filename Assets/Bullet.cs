using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifeTime = 2f;
    [SerializeField] int damage = 1; // Her mermi 1 hasar verir
    private float lifeTimer;

    void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Eğer çarptığı obje NPC ise hasar ver
        if (collision.collider.CompareTag("NPC"))
        {
            NPCHealth npcHealth = collision.collider.GetComponent<NPCHealth>();
            if (npcHealth != null)
            {
                npcHealth.TakeDamage(damage);
            }
        }

        // Çarptığında mermiyi yok et
        Destroy(gameObject);
    }
}
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    [Header("Sight Settings")]
    public float viewRadius = 10f;
    public float viewAngle = 90f;
    public LayerMask playerMask;
    public LayerMask obstacleMask;

    [HideInInspector] public bool canSeePlayer;
    [HideInInspector] public Transform player;

    void Start()
    {
        // Player objesini otomatik bul
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void Update()
    {
        CheckForPlayer();
    }

    void CheckForPlayer()
    {
        if (player == null)
        {
            canSeePlayer = false;
            return;
        }

        // Oyuncu menzilde mi?
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float distToPlayer = Vector3.Distance(transform.position, player.position);

        if (distToPlayer < viewRadius)
        {
            // Oyuncu görüş açısında mı?
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                // Arada engel var mı?
                if (!Physics.Raycast(transform.position + Vector3.up, dirToPlayer, distToPlayer, obstacleMask))
                {
                    canSeePlayer = true;
                    return;
                }
            }
        }

        canSeePlayer = false;
    }
}

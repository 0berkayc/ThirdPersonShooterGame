using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    [Header("Next States")]
    public AttackState attackState;
    public IdleState idleState;

    [Header("References")]
    public EnemySight enemySight;
    public NavMeshAgent agent;
    public Transform player;

    [Header("Parameters")]
    public float attackRange = 2f; // saldırı mesafesi

    public override State RunCurrentState()
    {
        if (enemySight != null && enemySight.canSeePlayer && player != null)
        {
            // oyuncunun peşinden git
            agent.SetDestination(player.position);

            // eğer oyuncuya yeterince yaklaştıysa saldır
            float distanceToPlayer = Vector3.Distance(agent.transform.position, player.position);
            if (distanceToPlayer <= attackRange)
            {
                return attackState;
            }

            return this;
        }
        else
        {
            // oyuncuyu göremiyorsa idle’a dön
            agent.ResetPath();
            return idleState;
        }
    }
}

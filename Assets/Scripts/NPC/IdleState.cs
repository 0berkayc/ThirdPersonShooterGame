using UnityEngine;

public class IdleState : State
{
    [Header("Next States")]
    public ChaseState chaseState;
    public PatrolState patrolState;

    [Header("References")]
    public EnemySight enemySight; // yeni eklendi

    public bool startPatrolling;

    public override State RunCurrentState()
    {
        if (enemySight != null && enemySight.canSeePlayer)
        {
            // Oyuncuyu gördüyse kovalamaya geç
            return chaseState;
        }
        else if (startPatrolling)
        {
            // Oyuncu yoksa devriyeye başla
            return patrolState;
        }
        else
        {
            // Olduğu yerde bekle
            return this;
        }
    }
}

using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    public IdleState idleState;
    public ChaseState chaseState;
    public bool canSeeThePlayer;

    private NavMeshAgent agent;
    public Transform[] patrolPoints;   // devriye gezeceği noktalar
    private int currentPointIndex;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[0].position);
        }
    }

    public override State RunCurrentState()
    {
        // Player'ı görüyorsa kovalamaya geç
        if (canSeeThePlayer)
        {
            return chaseState;
        }

        // Noktaya vardığında bir sonrakine geç
        if (!agent.pathPending && agent.remainingDistance < 0.3f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPointIndex].position);
        }

        // Her frame'de aktif kal
        return this;
    }
}

using UnityEngine;

public abstract class MovementBaseState
{
    public abstract void EnterState(MovementState movementState);

    public abstract void UpdateState(MovementState movementState);
}

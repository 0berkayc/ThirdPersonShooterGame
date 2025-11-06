using UnityEngine;

public class RunState : MovementBaseState
{
    public override void EnterState(MovementState state)
    {
        state.anim.SetBool("Running", true);
        state.anim.SetBool("Walking", false);
        state.anim.SetBool("Crouching", false);
    }

    public override void UpdateState(MovementState state)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
            ExitState(state, state.Walk);
        else if (state.moveDirection.magnitude < 0.1f)
            ExitState(state, state.Idle);

        state.currentMoveSpeed = state.vtInput < 0 ? state.runBackSpeed : state.runSpeed;
    }

    void ExitState(MovementState state, MovementBaseState newState)
    {
        state.anim.SetBool("Running", false);
        state.SwitchState(newState);
    }
}
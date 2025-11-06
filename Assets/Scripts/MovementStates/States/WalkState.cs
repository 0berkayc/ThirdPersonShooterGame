using UnityEngine;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementState state)
    {
        state.anim.SetBool("Walking", true);
        state.anim.SetBool("Running", false);
        state.anim.SetBool("Crouching", false);
    }

    public override void UpdateState(MovementState state)
    {
        if (Input.GetKey(KeyCode.LeftShift))
            ExitState(state, state.Run);
        else if (state.moveDirection.magnitude < 0.1f)
            ExitState(state, state.Idle);
        else if (Input.GetKeyDown(KeyCode.C))
            ExitState(state, state.Crouch);

        state.currentMoveSpeed = state.vtInput < 0 ? state.walkBackSpeed : state.walkSpeed;
    }

    void ExitState(MovementState state, MovementBaseState newState)
    {
        state.anim.SetBool("Walking", false);
        state.SwitchState(newState);
    }
}
using UnityEngine;

public class CrouchState : MovementBaseState
{
    public override void EnterState(MovementState state)
    {
        state.anim.SetBool("Crouching", true);

    }

    public override void UpdateState(MovementState state)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(state, state.Run);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (state.moveDirection.magnitude < 0.1f) ExitState(state, state.Idle);
            else ExitState(state, state.Walk);  
        }

        state.currentMoveSpeed = state.vtInput < 0 ? state.crouchBackSpeed : state.crouchSpeed;
    }

    void ExitState(MovementState state, MovementBaseState newState)
    {
        state.anim.SetBool("Crouching", false);
        state.SwitchState(newState);
    }
}
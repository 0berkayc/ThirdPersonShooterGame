using UnityEngine;

public class IdleState : MovementBaseState
{
    public override void EnterState(MovementState state)
    {
        state.anim.SetBool("Walking", false);
        state.anim.SetBool("Running", false);
        state.anim.SetBool("Crouching", false);
        state.currentMoveSpeed = 0f;
    }

    public override void UpdateState(MovementState state)
    {
        if (state.moveDirection.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                state.SwitchState(state.Run);
            else
                state.SwitchState(state.Walk);
        }

        if (Input.GetKeyDown(KeyCode.C))
            state.SwitchState(state.Crouch);
    }
}
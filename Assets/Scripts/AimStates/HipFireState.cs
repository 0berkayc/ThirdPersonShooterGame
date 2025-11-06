using UnityEngine;
using UnityEngine.InputSystem;

public class HipFireState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.anim.SetBool("Aiming", false);
        aim.currentFov = aim.hipFov;
    }
    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKey(KeyCode.P))
        {
            aim.SwitchState(aim.Aim);
        } 
    }
}

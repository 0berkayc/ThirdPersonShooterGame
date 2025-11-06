using UnityEngine;

public class AttackState : State
{
    public override State RunCurrentState()
    {
        return this; 
    }
}

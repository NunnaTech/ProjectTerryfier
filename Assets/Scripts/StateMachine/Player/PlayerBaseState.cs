using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerSateMachine stateMachine;
    public PlayerBaseState(PlayerSateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.CharacterController.Move((motion + stateMachine.forceReceiver.Movement) * deltaTime);
    }
    
    protected void Runnig()
    {
}
}

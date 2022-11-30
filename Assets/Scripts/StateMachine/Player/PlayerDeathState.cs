using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDeathState : PlayerBaseState
{
    private int deadHash = Animator.StringToHash("Death");

    public PlayerDeathState(PlayerSateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.Animator.Play(deadHash);
        stateMachine.death.Play();
    }

    public override void Exit()
    {
    }

    public override void Tick(float deltaTime)
    {
    }
}

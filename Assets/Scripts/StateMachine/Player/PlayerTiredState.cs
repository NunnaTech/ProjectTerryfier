using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTiredState : PlayerBaseState
{
    private readonly int tiredSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int tiredBlendTreeHash = Animator.StringToHash("TiredBlend Tree");
    private const float animatorDampTime = 0.05f;
    
    public PlayerTiredState(PlayerSateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void Enter()
    {
        stateMachine.Animator.Play(tiredBlendTreeHash);
    }

    public override void Exit()
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));

    }
    public override void Tick(float deltaTime)
    {
        if (stateMachine.staminaSlider.value ==stateMachine.maxStamina){
            Exit();
        }
    }
}

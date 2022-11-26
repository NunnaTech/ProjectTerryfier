using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTiredState : PlayerBaseState
{
    private readonly int tiredSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int fallFlatHash = Animator.StringToHash("fallFlat2");
    private const float animatorDampTime = 0.05f;
    
    public PlayerTiredState(PlayerSateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void Enter()
    {
        stateMachine.Animator.Play(fallFlatHash);
    }

    public override void Exit()
    {
        stateMachine.InputReader.RunEvent += OnRun;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.staminaSlider.value >= (stateMachine.maxStamina/2)){
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
    }
    private void OnRun(bool onRun)
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
}

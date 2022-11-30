using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerTiredState : PlayerBaseState
{
    private readonly int tiredSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int fallFlatHash = Animator.StringToHash("fallFlat2");
    private readonly int DeathHash = Animator.StringToHash("Death");

    private const float animatorDampTime = 0.05f;
    
    public PlayerTiredState(PlayerSateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void Enter()
    {
        stateMachine.takeDamage(30);
        stateMachine.DiedPlayer += activateMenu;
        if(stateMachine.health > 0){
            stateMachine.Animator.Play(fallFlatHash);
            stateMachine.fall.Play();
        }
        

    }

    private void activateMenu(object sender, EventArgs e)
    {
        stateMachine.menuGameOver.SetActive(true);
    }
    public override void Exit()
    {
    }

    public override void Tick(float deltaTime)
    {
        
        if (stateMachine.staminaSlider.value >= (stateMachine.maxStamina * 0.80)){
            stateMachine.isSprinting = false;
            stateMachine.FreeLookMovementSpeed = stateMachine.sprintSpeed;
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
    }
    private void OnRun(bool onRun)
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
}

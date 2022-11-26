using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerFreeLookState : PlayerBaseState
{
    // Animation
    private readonly int freeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int freeLookBlendTreeHash = Animator.StringToHash("FreeLookBlend Tree");
    private const float animatorDampTime = 0.05f;

    // Running Params
    public float sprintingSpeedMultiplier = 6f;
    private float sprintSpeed = 2f;
    

    
    public PlayerFreeLookState(PlayerSateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.Animator.Play(freeLookBlendTreeHash);
        stateMachine.InputReader.RunEvent += OnRun;
       

    }

    public override void Exit()
    {
        stateMachine.InputReader.RunEvent -= OnRun;
    }
    private void OnRun(bool isSprinting)
    {
        stateMachine.isSprinting = isSprinting;
        RunCheck();
    }

    public void RunCheck()
    {
        if(stateMachine.isSprinting==true)
        {
           stateMachine.UseStamina(stateMachine.staminaUseAmount);
           stateMachine.FreeLookMovementSpeed = sprintingSpeedMultiplier;
        }
        else
        {
           stateMachine.UseStamina(0);
           stateMachine.FreeLookMovementSpeed = sprintSpeed;
        }
    }


    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();
        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(freeLookSpeedHash, 0, animatorDampTime, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(freeLookSpeedHash, 1, animatorDampTime, deltaTime);
        FaceMovementDirection(movement);
        // Start to run
        if(stateMachine.isSprinting==true){
            stateMachine.Animator.SetFloat(freeLookSpeedHash, 2, animatorDampTime, deltaTime);
        }
        // fail
        if(stateMachine.isSprinting==true && stateMachine.currentStamina < 0.10){
            stateMachine.SwitchState(new PlayerTiredState(stateMachine));
        
        }
        
    }

   
    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y +
            right * stateMachine.InputReader.MovementValue.x;
    }
    private void FaceMovementDirection(Vector3 movement)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            Time.deltaTime * stateMachine.RotationDamping);
    }
}

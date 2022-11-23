using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int freeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private const float animatorDampTime = 0.05f;
    // Running
    public bool isSprinting;
    public float sprintingSpeedMultiplier = 6f;
    private float sprintSpeed = 2f;
    // Stamine
    public float staminaUseAmount = 5;
    public float maxStamina = 100;
    private float currentStamina;
    private float regenerateStaminaTime = 0.1f;
    private float regenerateAmount = 2;
    private float losingStaminaTime = 0.1f;

    private readonly int freeLookBlendTreeHash = Animator.StringToHash("FreeLookBlend Tree");
    
    public PlayerFreeLookState(PlayerSateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.Animator.Play(freeLookBlendTreeHash);
        stateMachine.InputReader.RunEvent += OnRun;
        currentStamina = maxStamina;
        stateMachine.staminaSlider.maxValue = maxStamina;
        stateMachine.staminaSlider.value = maxStamina;

    }

    public override void Exit()
    {

    }
    private void OnRun()
    {
        LosingStaminaCoroutine();
        RunCheck();
        RegenerateStamineCoroutine();
    }

    public void RunCheck()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = !isSprinting;
        }
        if (isSprinting==true)
        {
            stateMachine.FreeLookMovementSpeed = sprintingSpeedMultiplier;
        }else
        {
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
        if(isSprinting==true){
            stateMachine.Animator.SetFloat(freeLookSpeedHash, 2, animatorDampTime, deltaTime);
        }else{
        }
        
    }

    private void LosingStaminaCoroutine()
    {
        if (currentStamina > 0)
        {
            currentStamina -= staminaUseAmount;
            stateMachine.staminaSlider.value = currentStamina;
            new WaitForSeconds(losingStaminaTime);
        }
        
    }
    private void RegenerateStamineCoroutine()
    {
        if (currentStamina < maxStamina && !isSprinting)
        {
            currentStamina += regenerateAmount;
            stateMachine.staminaSlider.value = currentStamina;
            new WaitForSeconds(regenerateStaminaTime);
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

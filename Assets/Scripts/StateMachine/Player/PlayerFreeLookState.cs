using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int freeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private const float animatorDampTime = 0.05f;

    private readonly int freeLookBlendTreeHash = Animator.StringToHash("FreeLookBlend Tree");
    public PlayerFreeLookState(PlayerSateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.Animator.Play(freeLookBlendTreeHash);
    }

    public override void Exit()
    {

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
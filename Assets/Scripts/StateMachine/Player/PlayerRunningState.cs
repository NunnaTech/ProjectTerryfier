using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerBaseState
{
    private readonly int runningBlendTreeHash = Animator.StringToHash("RunnigBlend Tree");
    
    
    public PlayerRunningState(PlayerSateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        stateMachine.Animator.Play(runningBlendTreeHash);
        stateMachine.InputReader.RunEvent += OnCancel;
       
    }
      public void OnCancel()
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
    public override void Exit()
    {
        stateMachine.InputReader.RunEvent -= OnCancel;
    }
     public override void Tick(float deltaTime)
    {

    }

}

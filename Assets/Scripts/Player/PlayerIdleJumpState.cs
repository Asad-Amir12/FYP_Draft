using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using System.Timers;
public class PlayerIdleJumpState : PlayerBaseState
{
    
    private readonly int IdleJumpHash = Animator.StringToHash("IdleJump");
    private const float CrossFadeDuration = 0.5f;
    private System.Timers.Timer timer;

    private bool jumpPerformed = false;
  //  private Vector3 initialStateMachineVelocity = Vector3.zero;
    public PlayerIdleJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {   
        //initialStateMachineVelocity =  stateMachine.Velocity;
        StartCheckingAnimation();
       stateMachine.Animator.Play(IdleJumpHash);
       // % 1f * stateMachine.Animator.GetCurrentAnimatorStateInfo(0).length
      
    }

    public override void Tick()
    {
        ApplyGravity();

        if (stateMachine.Velocity.y <= 0f && jumpPerformed )
        {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }

        FaceMoveDirection();
        Move();
    }

    public override void Exit() { }

    public void AddJumpForce(){
        stateMachine.Velocity = new Vector3(0f, stateMachine.JumpForce, 0f);
    }

    public void StartCheckingAnimation()
    {
        // Initialize the timer with 100 milliseconds (0.1 seconds)
        timer = new System.Timers.Timer(400);
        timer.Elapsed += OnTimerElapsed;
        timer.AutoReset = false; // Ensure it only triggers once
        timer.Start();
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        timer.Stop();
        timer.Dispose();

        // Call the function after 0.1 seconds
        Debug.Log("delaYED JUMP ADDED");
       AddJumpForce();
       jumpPerformed= true;
    }


}

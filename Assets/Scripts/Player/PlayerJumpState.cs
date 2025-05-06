using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private readonly int JumpHash = Animator.StringToHash("RunningJump");

    private const float CrossFadeDuration = 0.1f;
  //  private Vector3 initialStateMachineVelocity = Vector3.zero;
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {   
        //initialStateMachineVelocity =  stateMachine.Velocity;
        stateMachine.Velocity = new Vector3(stateMachine.Velocity.x, stateMachine.JumpForce, stateMachine.Velocity.z);
        
        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFadeDuration);

       
    }

    public override void Tick()
    {
        ApplyGravity();

        if (stateMachine.Velocity.y <= 0f)
        {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }

        FaceMoveDirection();
        Move();
    }

    public override void Exit() { }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerIdleState : PlayerBaseState
// {
//     private readonly int IdleStateHash = Animator.StringToHash("Idle");
//     private const float AnimationDampTime = 0.1f;
//     private const float CrossFadeDuration = 0.1f;

//     public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

//     public override void Enter()
//     {
//         stateMachine.Velocity = Vector3.zero;

//         stateMachine.Animator.CrossFadeInFixedTime(IdleStateHash, CrossFadeDuration);

//         stateMachine.InputReader.OnJumpPerformed += SwitchToJumpState;
//     }

//     public override void Tick()
//     {
//         if (!stateMachine.Controller.isGrounded)
//         {
//             stateMachine.SwitchState(new PlayerFallState(stateMachine));
//         }
//         if(stateMachine.Controller.isGrounded && stateMachine.InputReader.OnMovePerformed)

//         CalculateMoveDirection();
//         FaceMoveDirection();
//         Move();

//         stateMachine.Animator.SetFloat(MoveSpeedHash, stateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? 1f : 0f, AnimationDampTime, Time.deltaTime);
//     }

//     public override void Exit()
//     {
//         stateMachine.InputReader.OnJumpPerformed -= SwitchToJumpState;
//     }

//     private void SwitchToJumpState()
//     {
//         stateMachine.SwitchState(new PlayerIdleJumpState(stateMachine));
//     }
//     // Start is called before the first frame update
   
//}

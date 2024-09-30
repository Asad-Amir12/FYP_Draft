using UnityEngine;

public class PlayerSprintState : PlayerBaseState
{
    // private readonly int SprintSpeedHash = Animator.StringToHash("SprintSpeed");
    // private readonly int MoveBlendTreeHash = Animator.StringToHash("MoveBlendTree");

    private readonly int SprintHash = Animator.StringToHash("Sprint");
    private const float AnimationDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    public PlayerSprintState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("entered sprint state");
        stateMachine.Velocity.y = Physics.gravity.y;

       stateMachine.Animator.Play("Sprint");

        stateMachine.InputReader.OnJumpPerformed += SwitchToJumpState;
    }

    public override void Tick()
    {
        // if (!stateMachine.Controller.isGrounded)
        // {
        //     stateMachine.SwitchState(new PlayerFallState(stateMachine));
        // }
        Debug.Log(stateMachine.Velocity.y);
        if(stateMachine.InputReader.MoveComposite.magnitude !=0 && !Input.GetKey(KeyCode.LeftShift)){
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }
        CalculateMoveDirection();
        FaceMoveDirection();
        Move();

       // stateMachine.Animator.SetFloat(SprintSpeedHash, stateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? 1f : 0f, AnimationDampTime, Time.deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.OnJumpPerformed -= SwitchToJumpState;
    }

    private void SwitchToJumpState()
    {
        if (stateMachine.InputReader.MoveComposite.magnitude != 0f)
        {

            stateMachine.SwitchState(new PlayerJumpState(stateMachine));
        }
        else
        {

            stateMachine.SwitchState(new PlayerIdleJumpState(stateMachine));
        }

    }
}
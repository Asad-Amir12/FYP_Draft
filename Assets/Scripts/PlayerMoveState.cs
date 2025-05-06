using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    private readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
    private readonly int MoveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
    private const float AnimationDampTime = 0.1f;
    private const float CrossFadeDuration = 0.5f;
    private readonly float coyoteTime = 0.6f; // Duration of coyote time
    private float coyoteTimer; // Timer to track coyote time

    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("enteredMoveState");
        stateMachine.Velocity.y = Physics.gravity.y;

        stateMachine.Animator.CrossFadeInFixedTime(MoveBlendTreeHash, CrossFadeDuration);

        stateMachine.InputReader.OnJumpPerformed += SwitchToJumpState;
        stateMachine.InputReader.OnRollPerformed += SwitchToRollState;
        coyoteTimer = 0f; // Reset the coyote timer when entering the state
    }

    public override void Tick()
    {

        if (!stateMachine.Controller.isGrounded)
        {
            coyoteTimer += Time.deltaTime; // Increment the coyote timer
            if (coyoteTimer >= coyoteTime) // Check if coyote time has expired
            {
                stateMachine.SwitchState(new PlayerFallState(stateMachine));
            }
        }
        else
        {
            coyoteTimer = 0f; // Reset the timer if grounded
        }

        if (stateMachine.InputReader.MoveComposite.magnitude != 0 && Input.GetKey(KeyCode.LeftShift))
        {
            stateMachine.SwitchState(new PlayerSprintState(stateMachine));
        }
        CalculateMoveDirection();
        FaceMoveDirection();
        Move();

        stateMachine.Animator.SetFloat(MoveSpeedHash, stateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? 1f : 0f, AnimationDampTime, Time.deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.OnJumpPerformed -= SwitchToJumpState;
        stateMachine.InputReader.OnRollPerformed -= SwitchToRollState;
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

    private void SwitchToRollState()
    {

        stateMachine.SwitchState(new PlayerRollState(stateMachine));
    }
}
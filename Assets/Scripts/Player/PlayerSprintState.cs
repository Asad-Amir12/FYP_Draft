using UnityEngine;

public class PlayerSprintState : PlayerBaseState
{
    // private readonly int SprintSpeedHash = Animator.StringToHash("SprintSpeed");
    // private readonly int MoveBlendTreeHash = Animator.StringToHash("MoveBlendTree");

    private readonly int SprintHash = Animator.StringToHash("Sprint");
    private readonly int MoveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
    private const float AnimationDampTime = 0.1f;
    private const float CrossFadeDuration = 0.3f;
    private float coyoteTime = 0.4f; // Duration of coyote time
    private float coyoteTimer; // Timer to track coyote time
    public PlayerSprintState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("entered sprint state");
        stateMachine.Velocity.y = Physics.gravity.y;
        stateMachine.Animator.CrossFadeInFixedTime(SprintHash, CrossFadeDuration);
        //stateMachine.Animator.Play("Sprint");

        stateMachine.InputReader.OnJumpPerformed += SwitchToJumpState;
        stateMachine.InputReader.OnRollPerformed += SwitchToRollState;
    }

    public override void Tick()
    {
        // if (!stateMachine.Controller.isGrounded)
        // {
        //     stateMachine.SwitchState(new PlayerFallState(stateMachine));
        // }
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

        if (stateMachine.InputReader.MoveComposite.magnitude != 0 && !Input.GetKey(KeyCode.LeftShift))
        {
            stateMachine.SwitchState(stateMachine.moveState);
        }
        if (stateMachine.InputReader.MoveComposite.magnitude == 0)
        {
            stateMachine.SwitchState(stateMachine.moveState);

        }
        CalculateSprintDirection();
        FaceMoveDirection();
        Move();

        // stateMachine.Animator.SetFloat(SprintSpeedHash, stateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? 1f : 0f, AnimationDampTime, Time.deltaTime);
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
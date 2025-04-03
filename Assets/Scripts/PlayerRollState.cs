using UnityEngine;

public class PlayerRollState : PlayerBaseState
{
    // private readonly int SprintSpeedHash = Animator.StringToHash("SprintSpeed");
    // private readonly int MoveBlendTreeHash = Animator.StringToHash("MoveBlendTree");

    private readonly int RollHash = Animator.StringToHash("RunToRoll");
    private const float AnimationDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    public PlayerRollState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    AnimatorStateInfo asi;
    public override void Enter()
    {
        Debug.Log("entered roll state");
        stateMachine.Velocity.y = Physics.gravity.y;

        stateMachine.Animator.Play("RunToRoll");


        // stateMachine.InputReader.OnJumpPerformed += SwitchToJumpState;
    }

    public override void Tick()
    {
        // if (!stateMachine.Controller.isGrounded)
        // {
        //     stateMachine.SwitchState(new PlayerFallState(stateMachine));
        // }

        CalculateMoveDirection();
        FaceMoveDirection();
        Move();
        asi = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        if (asi.normalizedTime >= 1f && !stateMachine.Animator.IsInTransition(0))
        {
            Exit();

            if (stateMachine.InputReader.MoveComposite.magnitude != 0f && Input.GetKeyDown(KeyCode.LeftShift))
            {
                stateMachine.SwitchState(new PlayerSprintState(stateMachine));


            }
            else
            {

                stateMachine.SwitchState(new PlayerMoveState(stateMachine));
            }


        }
        // stateMachine.Animator.SetFloat(SprintSpeedHash, stateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? 1f : 0f, AnimationDampTime, Time.deltaTime);
    }

    public override void Exit()
    {

    }

    // private void SwitchToJumpState()
    // {
    // if (stateMachine.InputReader.MoveComposite.magnitude != 0f)
    // {

    //     stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    // }


    // }
}
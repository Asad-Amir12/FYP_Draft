using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class PlayerStateMachine : StateMachine
{
    public Vector3 Velocity;
    public float MovementSpeed { get; private set; } = 3f;
    public float JumpForce { get; private set; } = 5f;
    public float LookRotationDampFactor { get; private set; } = 10f;
    public Transform MainCamera { get; private set; }
    public InputReader InputReader { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public readonly float AttackDamage = 20f; //Base attack dmg 
    public PlayerAttackState attackState;
    public PlayerMoveState moveState;
    public bool IsAttacking = false;

    [Header("Combat Data")]
    [SerializeField] private AttackComboData attackComboData;

    [SerializeField] private SwordHitDetector SwordDetector;
    public float SprintSpeed { get; private set; } = 10f;

    private void SubscribeToInputActions()
    {
        InputReader.OnAttackPerformed += SwitchToAttackState;
    }
    private void Start()
    {
        MainCamera = Camera.main.transform;
        InputReader = GetComponent<InputReader>();
        Animator = GetComponent<Animator>();
        Controller = GetComponent<CharacterController>();
        attackState = new PlayerAttackState(this, SwordDetector, attackComboData);
        moveState = new PlayerMoveState(this);
        SubscribeToInputActions();
        SwitchState(moveState);
    }

    private void SwitchToAttackState()
    {
        // If we’re already in attackState, just buffer the next combo
        if (IsAttacking)
        {
            attackState.BufferNextCombo();
        }
        else
        {
            // Otherwise, enter attackState fresh
            SwitchState(attackState);
        }
    }


    private void OnDestroy()
    {
        InputReader.OnAttackPerformed -= SwitchToAttackState;
    }
}
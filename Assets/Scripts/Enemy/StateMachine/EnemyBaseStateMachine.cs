using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyBaseStateMachine : EnemyStateMachine<EnemyBaseStateMachine>
{
    [SerializeField] private NavMeshAgent agent;
    public Collider DetectionZone { get; set; }
    public Collider AttackZone { get; set; }
    [SerializeField] private Transform player;
    public bool ShouldAttack { get; private set; } = false;
    [SerializeField] private AttackData attackData;
    //States
    [SerializeField] public Animator Animator;
    [SerializeField] public EnemyIdleState IdleState;
    [SerializeField] public EnemyMoveState MoveState;
    [SerializeField] public EnemyAttackState AttackState;
    public AnimationEvents animationEvents;
    public new EnemyState<EnemyBaseStateMachine> CurrentState => base.CurrentState;

    private void InitializeStates()
    {
        IdleState = new EnemyIdleState(this, agent, player);
        MoveState = new EnemyMoveState(this, agent, player);
        AttackState = new EnemyAttackState(this, agent, player, attackData);
    }

    private void Awake()
    {


    }

    private void Start()
    {
        animationEvents = GetComponent<AnimationEvents>();
        player = CowboyReferenceHolder.Instance.CowboyTransform;
        IdleState = new EnemyIdleState(this, agent, player);
        InitializeStates();
        TransitionToState(MoveState);
        EventBus.OnPlayerAttacked += OnPlayerAttacked;
    }
    public void ChangeState(EnemyState<EnemyBaseStateMachine> newState)
    {
        TransitionToState(newState);
    }

    public void HandleZoneTrigger(ZoneTrigger.ZoneType zone, bool entered)
    {
        //Debug.Log($"ZoneTrigger: {zone} entered={entered} in state {CurrentState}");

        if (zone == ZoneTrigger.ZoneType.Detection && entered)
        {
            ChangeState(MoveState);
        }
        else if (zone == ZoneTrigger.ZoneType.Attack && entered)
        {
            Debug.Log($"ZoneTrigger: {zone} entered={entered} in state {CurrentState}");
            if (entered)
                ShouldAttack = true;
            else
                ShouldAttack = false;

            ChangeState(AttackState);
        }
        else if (zone == ZoneTrigger.ZoneType.Attack && !entered)
        {
            ChangeState(MoveState);
        }
        // add more logic as needed…
    }

    void OnPlayerAttacked()
    {
        //TODO remove const expression
        Debug.Log("Player attacked, triggering health change.");
        EventBus.TriggerOnPlayerHealthChanged(-10);

    }
    void OnDestroy()
    {
        EventBus.OnPlayerAttacked -= OnPlayerAttacked;
    }
}

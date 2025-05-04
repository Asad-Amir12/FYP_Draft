using UnityEngine;
using UnityEngine.AI;
public class EnemyBaseStateMachine : EnemyStateMachine<EnemyBaseStateMachine>
{
    [SerializeField] private NavMeshAgent agent;
    public Collider DetectionZone { get; set; }
    public Collider AttackZone { get; set; }
    [SerializeField] private Transform player;
    public bool ShouldAttack { get; private set; } = false;

    //States
    [SerializeField] public Animator Animator;
    [SerializeField] public EnemyIdleState IdleState;
    [SerializeField] public EnemyMoveState MoveState;
    [SerializeField] public EnemyAttackState AttackState;

    public new EnemyState<EnemyBaseStateMachine> CurrentState => base.CurrentState;

    private void InitializeStates()
    {
        IdleState = new EnemyIdleState(this, agent, player);
        MoveState = new EnemyMoveState(this, agent, player);
        AttackState = new EnemyAttackState(this, agent, player);
    }

    private void Awake()
    {

        IdleState = new EnemyIdleState(this, agent, player);

    }

    private void Start()
    {
        InitializeStates();
        TransitionToState(MoveState);
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
}

using UnityEngine;
using UnityEngine.AI;
public class EnemyIdleState : EnemyState<EnemyBaseStateMachine>
{
    private readonly int IdleStateHash = Animator.StringToHash("Idle");
    private new readonly EnemyBaseStateMachine Owner;
    private readonly NavMeshAgent agent;
    private readonly Transform player;
    public EnemyIdleState(EnemyBaseStateMachine sm, NavMeshAgent agent, Transform player) : base(sm)
    {
        Owner = sm;
        this.agent = agent;
        this.player = player;
    }

    public override void Enter()
    {
        agent.isStopped = true;
        Owner.Animator.Play(IdleStateHash);
    }

    public override void Tick()
    {

    }

    public override void Exit()
    {

    }
}

using RPGCharacterAnims.Actions;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMoveState : EnemyState<EnemyBaseStateMachine>
{
    private readonly int MoveStateHash = Animator.StringToHash("Move");
    private new readonly EnemyBaseStateMachine Owner;
    private Collider detectionZone;
    private readonly NavMeshAgent agent;
    private readonly Transform player;
    public EnemyMoveState(EnemyBaseStateMachine sm, NavMeshAgent agent, Transform player) : base(sm)
    {
        Owner = sm;
        this.agent = agent;
        this.player = player;
        detectionZone = Owner.DetectionZone;
    }

    public override void Enter()
    {
        if (agent != null && player != null)
        {
            // agent.isStopped = false;
            agent.SetDestination(player.position);
            Owner.Animator.Play(MoveStateHash);
        }
    }


    public override void Tick()
    {
        if (agent != null && player != null)
        {
            float distanceToPlayer = Vector3.Distance(agent.transform.position, player.position);

            // Update destination if the player has moved significantly
            if (distanceToPlayer > agent.stoppingDistance)
            {
                agent.SetDestination(player.position);
            }


        }
    }

    public override void Exit()
    {
        if (agent != null)
        {
            //agent.isStopped = true;
        }
    }

}

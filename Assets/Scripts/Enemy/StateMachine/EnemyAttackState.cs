using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyState<EnemyBaseStateMachine>
{
    private readonly int AttackStateHash = Animator.StringToHash("Attack");
    private readonly NavMeshAgent agent;
    private readonly Transform player;
    private readonly Animator animator;
    private float attackCooldown = 1.5f;
    private float lastAttackTime = -Mathf.Infinity;
    private float attackRange = 2.0f;
    private readonly Collider attackZone;
    private bool isAttacking = false;

    public EnemyAttackState(EnemyBaseStateMachine sm, NavMeshAgent agent, Transform player) : base(sm)
    {
        this.agent = agent;
        this.player = player;
        this.animator = Owner.Animator;
        attackZone = Owner.AttackZone;
    }

    public override void Enter()
    {

        if (agent != null)
        {
            agent.isStopped = true;
        }


    }

    public override void Tick()
    {
        if (player == null || agent == null)
            return;



        if (!Owner.ShouldAttack)
        {
            // Transition back to move state if player is out of range
            Owner.ChangeState(new EnemyMoveState(Owner, agent, player));
            return;
        }

        // Face the player
        Vector3 direction = (player.position - agent.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, Time.deltaTime * 7f);

        // Attack if cooldown has passed
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    public override void Exit()
    {
        if (agent != null)
        {
            agent.isStopped = false;
        }

        if (attackZone != null)
        {
            attackZone.enabled = false;
        }
    }


    private void Attack()
    {
        if (animator != null)
        {
            animator.Play(AttackStateHash);

        }

        if (attackZone != null)
        {
            attackZone.enabled = true;
        }
    }




}

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyState<EnemyBaseStateMachine>
{
    private readonly int[] comboHashes;
    private int comboIndex;
    private readonly NavMeshAgent agent;
    private readonly Transform player;
    private readonly Animator animator;
    private float lastAttackTime = -Mathf.Infinity;

    // New: ScriptableObject reference
    private readonly AttackData data;
    private readonly Collider attackZone;

    public EnemyAttackState(EnemyBaseStateMachine sm, NavMeshAgent agent, Transform player, AttackData attackData) : base(sm)
    {
        this.agent = agent;
        this.player = player;
        this.animator = Owner.Animator;
        this.data = attackData;
        this.attackZone = Owner.AttackZone;

        // Precompute animator hashes if combos exist
        if (data.HasCombo)
        {
            comboHashes = new int[data.comboClips.Length];
            for (int i = 0; i < data.comboClips.Length; i++)
                comboHashes[i] = Animator.StringToHash(data.comboClips[i].name);
        }
    }

    public override void Enter()
    {
        Debug.Log("Entering Attack State");
        if (agent != null)
            agent.isStopped = true;

        comboIndex = 0;
    }

    public override void Tick()
    {
        if (player == null || agent == null)
            return;

        if (!Owner.ShouldAttack)
        {
            Owner.ChangeState(Owner.MoveState);
            return;
        }

        // Face the player
        Vector3 dir = (player.position - agent.transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRot, Time.deltaTime * 7f);

        // Check cooldown
        if (Time.time < lastAttackTime + data.cooldown)
            return;

        // Perform attack
        if (data.HasCombo)
            PerformCombo();
        else
            PerformSingle();

        lastAttackTime = Time.time;
    }

    public override void Exit()
    {
        if (agent != null)
            agent.isStopped = false;

        if (attackZone != null)
            attackZone.enabled = false;
    }

    private void PerformSingle()
    {
        int hash = Animator.StringToHash("Attack");
        animator.Play(hash);

        if (attackZone != null)
            attackZone.enabled = true;
    }

    private void PerformCombo()
    {
        if (comboIndex >= comboHashes.Length)
        {
            // Combo finished, exit or reset
            comboIndex = 0;
            return;
        }

        int currentHash = comboHashes[comboIndex];
        animator.Play(currentHash);
        if (attackZone != null)
            attackZone.enabled = true;

        comboIndex++;
    }


}

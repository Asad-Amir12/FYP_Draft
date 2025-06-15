using System;
using System.Linq;
using UnityEngine;


public class PlayerAttackState : PlayerBaseState
{
    private readonly AttackComboData comboData;
    //private readonly SwordHitDetector swordDetector;
    private readonly int[] comboHashes;
    private int currentComboIndex;
    private float comboTimer;
    private bool nextBuffered;

    public event Action<int> OnComboStep;

    public PlayerAttackState(PlayerStateMachine sm, SwordHitDetector det, AttackComboData data)
      : base(sm)
    {
        // swordDetector = det;
        comboData = data;
        comboHashes = data.comboClipNames
            .Select(Animator.StringToHash)
            .ToArray();
    }

    public override void Enter()
    {
        stateMachine.IsAttacking = true;
        currentComboIndex = 0;
        comboTimer = 0f;
        nextBuffered = false;
        //swordDetector.EnableDetector();
        // swordDetector.OnSwordHit += OnHit;
        stateMachine.InputReader.OnAttackPerformed += () => nextBuffered = true;
        PlayCombo();
        Debug.Log("Entering Attack State");
    }

    private void PlayCombo()
    {
        OnComboStep?.Invoke(currentComboIndex);
        stateMachine.Animator.Play(
            comboHashes[currentComboIndex]);
        comboTimer = -0.2f;
        Debug.Log($"Playing combo step {currentComboIndex}");
    }

    public override void Tick()
    {
        comboTimer += Time.deltaTime;
        var info = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        bool finished = info.shortNameHash == comboHashes[currentComboIndex]
                        && info.normalizedTime >= 1f;

        if (DamageDealer.Instance != null)
        {
            DamageDealer.Instance.PlayerAtkDmg = comboData.damagePerComboStep[currentComboIndex];
        }

        if (finished)
        {
            if (nextBuffered
                && comboTimer <= comboData.comboResetTime
                && currentComboIndex < comboHashes.Length - 1)
            {
                currentComboIndex++;
                nextBuffered = false;
                PlayCombo();
            }
            else
            {
                stateMachine.SwitchState(stateMachine.moveState);
            }
        }
    }

    public override void Exit()
    {
        // swordDetector.OnSwordHit -= OnHit;
        stateMachine.InputReader.OnAttackPerformed -= () => nextBuffered = true;
        //  swordDetector.DisableDetector();
        stateMachine.IsAttacking = false;
    }

    private void OnHit(Collider enemy)
    {
        float dmg = comboData.damagePerComboStep.Length > currentComboIndex
            ? comboData.damagePerComboStep[currentComboIndex]
            : stateMachine.AttackDamage;
        //enemy.GetComponent<EnemyHealth>()?.TakeDamage(dmg);
    }

    public void BufferNextCombo()
    {
        // Only buffer if still within the combo window
        if (comboTimer <= comboData.comboResetTime)
            nextBuffered = true;
    }
}

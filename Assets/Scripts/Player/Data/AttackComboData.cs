using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackComboData", menuName = "Game/Attack Combo Data", order = 1)]
public class AttackComboData : ScriptableObject
{
    [Header("Combo Animation Clips")]
    [Tooltip("List your combo clip names (must match Animator state names)")]
    public string[] comboClipNames;

    [Header("Timing Settings")]
    [Tooltip("Max time between presses to chain combos")]
    public float comboResetTime = 0.8f;

    [Header("Damage Settings")]
    [Tooltip("Damage applied per hit in this combo")]
    public int[] damagePerComboStep;
}

using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Game/Attack Data", order = 1)]
public class AttackData : ScriptableObject
{
    [Header("General Settings")]
    public float cooldown = 1.5f;
    public float range = 2.0f;

    [Header("Combo Settings (leave empty for single attack)")]
    public AnimationClip[] comboClips;

    /// <summary>
    /// Checks if this attack data defines a combo sequence
    /// </summary>
    public bool HasCombo => comboClips != null && comboClips.Length > 1;
}
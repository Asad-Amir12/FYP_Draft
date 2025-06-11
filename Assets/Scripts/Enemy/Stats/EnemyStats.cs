using System;
using System.Collections;
using UnityEngine;


public class EnemyStats : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private float invincibilityDuration = 0.1f;

    [Header("Combat Settings")]
    [SerializeField] private int baseDamage = 10;
    [SerializeField] private float defense = 0f;      // Flat damage reduction or multiplier

    public int currentHealth;
    // Exposed properties if you need to read them
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsAlive => currentHealth > 0;
    private bool isInvincible = false;

    // Events to notify other systems (e.g. UI, audio, spawn loot)
    public event Action<int> OnHealthChanged;
    public event Action OnDeath;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Call this to deal damage (negative delta) or heal (positive delta).
    /// </summary>
    public void ModifyHealth(int delta)
    {
        // If taking damage and currently invincible, ignore
        if (delta < 0 && isInvincible)
            return;

        // Apply defense (flat reduction)
        if (delta < 0)
            delta += Mathf.CeilToInt(defense);

        currentHealth = Mathf.Clamp(currentHealth + delta, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth);

        if (delta < 0)
            StartCoroutine(InvincibilityRoutine());

        if (currentHealth == 0)
            Die();
    }

    /// <summary>
    /// How much damage this enemy deals per attack.
    /// </summary>
    public int GetDamage()
    {
        return baseDamage;
    }

    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;
        // optional: flash sprite or play sound here
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    private void Die()
    {
        OnDeath?.Invoke();
        // default: destroy gameobject—override in subclasses if needed
        Destroy(gameObject);
    }


}

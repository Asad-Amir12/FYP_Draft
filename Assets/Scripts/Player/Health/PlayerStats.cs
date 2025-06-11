using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float invincibilityDuration = 0.3f;
    private bool isInvincible = false;

    public int MaxHealth = 200;
    private int currentHealth;
    // Start is called before the first frame update


    void Start()
    {

        EventBus.OnPlayerHealthChanged += OnPlayerHealthChanged;
        currentHealth = MaxHealth;
        healthSlider.maxValue = MaxHealth;
        healthSlider.value = currentHealth;
    }

    private void OnPlayerHealthChanged(int delta)
    {
        // If delta is negative (damage) and we're currently invincible, ignore it
        if (delta < 0 && isInvincible)
            return;

        // Apply health change
        currentHealth += delta;
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
        healthSlider.value = currentHealth;

        // If we just took damage, kick off invincibility
        if (delta < 0)
            StartCoroutine(InvincibilityCoroutine());
    }
    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }


    void OnDestroy()
    {
        // Always unsubscribe to avoid leaks
        EventBus.OnPlayerHealthChanged -= OnPlayerHealthChanged;
    }
}

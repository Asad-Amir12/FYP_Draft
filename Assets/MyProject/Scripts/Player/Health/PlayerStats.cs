using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInfo : MonoBehaviour
{
    // [SerializeField] private Slider healthSlider;
    [SerializeField] private float invincibilityDuration = 0.3f;
    private bool isInvincible = false;

    private int MaxHealth;
    private int currentHealth;
    private readonly float KeyDelay = 0.5f;
    private bool CanPressKey = true;
    // Start is called before the first frame update
    public static event Action<int> OnUpdateHealth;
    public static event Action<int> OnConsumableUsed;
    private bool hudPaused = false;

    void Start()
    {
        MaxHealth = DataCarrier.PlayerMaxHealth;
        EventBus.OnPlayerHealthChanged += OnPlayerHealthChanged;
        currentHealth = MaxHealth;
        EventBus.OnInventoryOpened += OnPauseHUDActions;
        EventBus.OnInventoryClosed += OnResumeHUDActions;

    }

    private void OnPlayerHealthChanged(int delta)
    {
        // If delta is negative (damage) and we're currently invincible, ignore it
        if (delta < 0 && isInvincible)
            return;

        // Apply health change
        currentHealth += delta;
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
        OnUpdateHealth?.Invoke(currentHealth);
        // healthSlider.value = currentHealth;
        if (currentHealth <= 0)
        {
            EventBus.TriggerOnPlayerDied();

        }
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

    void Update()
    {
        if (!hudPaused && !DataCarrier.isGamePaused)
        {

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                EventBus.TriggerOnInventoryOpened();

            }
        }
        if (CanPressKey)
        {

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                Debug.Log("Key 1 pressed");
                OnConsumableUsed?.Invoke(1);
                StartCoroutine(CoyoteTimer());
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {

                Debug.Log("Key 2 pressed");
                OnConsumableUsed?.Invoke(2);
                StartCoroutine(CoyoteTimer());
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {

                Debug.Log("Key 3 pressed");
                OnConsumableUsed?.Invoke(3);
                StartCoroutine(CoyoteTimer());
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {

                Debug.Log("Key 4 pressed");
                OnConsumableUsed?.Invoke(4);
                StartCoroutine(CoyoteTimer());
            }

        }
    }
    void OnDestroy()
    {
        // Always unsubscribe to avoid leaks
        EventBus.OnPlayerHealthChanged -= OnPlayerHealthChanged;
        StopAllCoroutines();
    }
    void OnPauseHUDActions()
    {
        CanPressKey = false;
        hudPaused = true;
    }
    void OnResumeHUDActions()
    {
        StartCoroutine(CoyoteTimer());
        hudPaused = false;
    }
    IEnumerator CoyoteTimer()
    {
        CanPressKey = false;
        yield return new WaitForSeconds(KeyDelay);
        CanPressKey = true;
    }
}

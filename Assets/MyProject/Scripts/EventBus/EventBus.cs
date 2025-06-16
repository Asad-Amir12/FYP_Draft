using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus
{

    public static event Action GamePaused;
    public static event Action GameResumed;
    public static event Action OnInventoryOpened;
    public static event Action OnInventoryClosed;
    public static event Action ReturnToMainMenu;
    public static event Action OnPlayerAttacked;
    public static event Action<int> OnPlayerHealthChanged;
    public static event Action OnEnemyAttacked;
    public static event Action OnEnemyKilled;
    public static event Action OnLevelCleared;
    public static event Action OnLevelFailed;
    public static event Action OnPlayerStatsChanged;
    public static event Action OnPlayerAttackDataChanged;
    public static event Action OnLevelGenerated;
    public static event Action OnPlayerDied;


    public static void TriggerGameResumed()
    {
        GameResumed?.Invoke();

    }
    public static void TriggerGamePaused()
    {
        GamePaused?.Invoke();
    }
    public static void TriggerOnPlayerDied()
    {
        OnPlayerDied?.Invoke();
    }
    public static void TriggerOnLevelGenerated()
    {
        OnLevelGenerated?.Invoke();
    }
    public static void TriggerReturnToMainMenu()
    {
        ReturnToMainMenu?.Invoke();
    }
    public static void TriggerOnPlayerStatsChanged()
    {
        OnPlayerStatsChanged?.Invoke();
    }
    public static void TriggerOnPlayerAttackDataChanged()
    {
        OnPlayerAttackDataChanged?.Invoke();
    }
    public static void TriggerOnPlayerHealthChanged(int change)
    {
        OnPlayerHealthChanged?.Invoke(change);
    }
    public static void TriggerOnPlayerAttacked()
    {
        OnPlayerAttacked?.Invoke();
    }
    public static void TriggerOnEnemyAttacked()
    {
        OnEnemyAttacked?.Invoke();
    }
    public static void TriggerOnEnemyKilled()
    {
        OnEnemyKilled?.Invoke();
    }

    public static void TriggerOnLevelCleared()
    {
        OnLevelCleared?.Invoke();
    }
    public static void TriggerOnLevelFailed()
    {
        OnLevelFailed?.Invoke();
    }
    public static void TriggerOnInventoryOpened()
    {
        OnInventoryOpened?.Invoke();
    }
    public static void TriggerOnInventoryClosed()
    {
        OnInventoryClosed?.Invoke();
    }
}

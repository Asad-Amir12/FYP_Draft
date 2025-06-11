using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus
{

    public static event Action GamePaused;
    public static event Action GameResumed;

    public static event Action ReturnToMainMenu;
    public static event Action OnPlayerAttacked;
    public static event Action<int> OnPlayerHealthChanged;
    public static event Action OnEnemyAttacked;

    public static void TriggerGameResumed()
    {
        GameResumed?.Invoke();

    }
    public static void TriggerGamePaused()
    {
        GamePaused?.Invoke();
    }

    public static void TriggerReturnToMainMenu()
    {
        ReturnToMainMenu?.Invoke();
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
}

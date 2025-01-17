using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus
{

    public static event Action GamePaused;
    public static event Action GameResumed;

    public static event Action ReturnToMainMenu;

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
}

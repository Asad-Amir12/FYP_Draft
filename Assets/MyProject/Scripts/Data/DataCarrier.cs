using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataCarrier
{
    public static int PlayerHealth { get; set; } = 100;
    public static int PlayerMaxHealth { get; set; } = 100;
    public static int PlayerStamina { get; set; } = 100;
    public static int PlayerAttack { get; set; } = 1;
    public static float PlayerAttackMultiplier { get; set; } = 5;
    public static int PlayerDefense { get; set; } = 0;
    public static int PlayerMaxStamina { get; set; } = 100;
    public static int PlayerCurrency { get; set; } = 0;
    public static int SelectedLevelIndex { get; set; } = 0;
    public static bool isGamePaused { get; set; } = false;
    public static void ResetData()
    {
        PlayerHealth = 100;
        PlayerMaxHealth = 100;
        PlayerStamina = 100;
        PlayerAttack = 1;
        PlayerAttackMultiplier = 5;
        PlayerDefense = 0;
        PlayerMaxStamina = 100;
        PlayerCurrency = 0;
        SelectedLevelIndex = 0;
    }
}


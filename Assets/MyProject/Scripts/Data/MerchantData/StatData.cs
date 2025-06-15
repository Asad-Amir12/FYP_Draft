using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Stats/StatData")]
public class StatData : ScriptableObject
{
    public int defense;
    public int attack;
    public int health;
    public int stamina;
    public int baseCost = 50;
    public float costMultiplier = 1f;

    public int GetCost(StatType type)
    {
        //  int current = GetValue(type);
        return baseCost;
    }
    public int GetValue(StatType type)
    {
        return type switch
        {
            StatType.DEFENSE => defense,
            StatType.ATTACK => attack,
            StatType.HEALTH => health,
            StatType.STAMINA => stamina,
            _ => 0
        };
    }

    public void Increase(StatType type)
    {
        switch (type)
        {
            case StatType.DEFENSE: defense++; break;
            case StatType.ATTACK: attack++; break;
            case StatType.HEALTH: health++; break;
            case StatType.STAMINA: stamina++; break;
        }
    }
}


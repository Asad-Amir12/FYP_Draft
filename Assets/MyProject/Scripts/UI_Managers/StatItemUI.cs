using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum StatType
{
    DEFENSE, ATTACK, HEALTH, STAMINA
}
[System.Serializable]
public class StatItemUI
{
    public StatType statType;
    public TextMeshProUGUI statValueText;
    public string statDescriptionText;
    public TextMeshProUGUI statCostText;
    public Button increaseCountButton;
    public Button decreaseCountButton;
    public int value;
}

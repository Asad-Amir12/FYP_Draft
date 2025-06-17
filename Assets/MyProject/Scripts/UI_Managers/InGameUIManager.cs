using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    [Header("Game Won Panel")]
    [SerializeField] private GameObject GameWonPanel;

    [SerializeField] private GameObject GameLostPanel;
    public int LastGivenReward = 0;
    public static InGameUIManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        EventBus.OnLevelFailed += ShowGameLostPanel;
        EventBus.OnLevelCleared += ShowGameWonPanel;
        InventoryManager.OnRewardsGiven += SetRewardValue;
    }

    public void SetRewardValue()
    {
        LastGivenReward = InventoryManager.Instance.LastReward;
    }
    public void ShowGameWonPanel()
    {

        GameWonPanel.SetActive(true);
        EventBus.OnLevelCleared -= ShowGameWonPanel;
    }
    public void HideGameWonPanel()
    {

        GameWonPanel.SetActive(false);
    }
    public void ShowGameLostPanel()
    {

        GameLostPanel.SetActive(true);
        EventBus.OnLevelFailed -= ShowGameLostPanel;
    }
    public void HideGameLostPanel()
    {

        GameLostPanel.SetActive(false);
    }
    void OnDestroy()
    {
        EventBus.OnLevelCleared -= ShowGameWonPanel;
        EventBus.OnLevelFailed -= ShowGameLostPanel;
        InventoryManager.OnRewardsGiven -= SetRewardValue;
    }
}



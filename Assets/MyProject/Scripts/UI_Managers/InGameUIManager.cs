using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    [Header("Game Won Panel")]
    [SerializeField] private GameObject GameWonPanel;

    [SerializeField] private GameObject GameLostPanel;

    public static InGameUIManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        EventBus.OnLevelFailed += ShowGameLostPanel;
        EventBus.OnLevelCleared += ShowGameWonPanel;
    }

    public void ShowGameWonPanel()
    {

        GameWonPanel.SetActive(true);
        EventBus.OnLevelFailed -= ShowGameWonPanel;
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
        EventBus.OnLevelFailed -= ShowGameWonPanel;
        EventBus.OnLevelFailed -= ShowGameLostPanel;
    }
}



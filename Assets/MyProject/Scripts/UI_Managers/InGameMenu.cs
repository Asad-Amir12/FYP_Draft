using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public static InGameMenu _Instance;
    public static event Action ResumeButtonClicked;
    public static InGameMenu Instance
    {
        get
        {

            if (_Instance == null)
            {

                Debug.LogError("InGameMenu is NULL");
            }

            return _Instance;
        }
    }

    void Awake()
    {
        _Instance = this;
        EventBus.GameResumed += GameResumed;
    }
    void OnEnable()
    {
        EventBus.TriggerGamePaused();
        SoundManager.Instance.PlaySoundWithFade(SoundData.MenuBGM, true);
    }

    void OnDisable()
    {
        EventBus.TriggerGameResumed();
        SoundManager.Instance.StopSoundWithFade(5f);
    }
    public void OnResumeButtonClicked()
    {
        EventBus.TriggerGameResumed();
        MainMenuUI.Instance.ResumeGameForcefully();
        Debug.Log("Resume Button Clicked EVent Invoked");
    }
    public void OnReturnToMainMenuButtonCliked()
    {
        Destroy(this.gameObject);
        LoadingScene.Instance.LoadScene(1);
        EventBus.TriggerReturnToMainMenu();


    }

    void GameResumed()
    {
        gameObject.SetActive(false);
    }
    void OnDestroy()
    {
        EventBus.GameResumed -= GameResumed;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

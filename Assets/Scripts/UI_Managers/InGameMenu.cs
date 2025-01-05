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
        
    }

    public void OnResumeButtonClicked()
    {
        EventBus.TriggerGameResumed();
        Debug.Log("Resume Button Clicked EVent Invoked");
    }
    public void OnReturnToMainMenuButtonCliked(){
    
       LoadingScene.Instance.LoadScene(1);
       
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public static InGameMenu _Instance;

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

    void OnReturnToMainMenuButtonCliked(){
    
       LoadingScene.Instance.LoadScene(2);
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

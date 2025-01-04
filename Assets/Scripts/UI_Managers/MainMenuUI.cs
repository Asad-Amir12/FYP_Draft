using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public static MainMenuUI _Instance;

    public static MainMenuUI Instance
    {
        get
        {
            if (_Instance == null)
            {
                Debug.LogError("MainMenuUI is NULL");
            }
            return _Instance;
        }
    }

   

    private void Awake()
    {
        _Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }

}

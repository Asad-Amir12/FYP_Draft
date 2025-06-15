using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public void OnStartButtonCLicked()
    {
        LoadingScene.Instance.LoadScene(1);
    }

    public void OnQuitButtonCLicked()
    {
        Application.Quit();
    }

    public void OnOptionsButtonCLicked()
    {
        Debug.Log("Options Button Clicked");
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

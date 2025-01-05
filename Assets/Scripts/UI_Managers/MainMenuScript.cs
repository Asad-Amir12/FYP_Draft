using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{

    public void OnStartButtonCLicked(){
        LoadingScene.Instance.LoadScene(2);
    }

    public void OnQuitButtonCLicked(){
        Application.Quit();
    }

    public void OnOptionsButtonCLicked(){
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] Image loadingFill;
    //[SerializeField] private GameObject mainMenuButtons;

    public static event Action OnGameSceneLoaded;
    public static event Action OnReturnToMainMenu;

    public static LoadingScene Instance;
    public void Awake(){
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    IEnumerator LoadLevelAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            //Debug.Log("loadingProgress: " + progress);
            loadingFill.fillAmount = progress;
            yield return null;
        }
        loadingScreen.SetActive(false);
        if(sceneIndex == 2){
             OnGameSceneLoaded?.Invoke();
        }
        if(SceneManager.GetActiveScene().buildIndex == 1 && sceneIndex == 1){
            OnReturnToMainMenu?.Invoke();
        }
       
    }
    public void LoadScene(int sceneIndex)
    {
        //gameObject.SetActive(true);
        loadingScreen.SetActive(true);
       // mainMenuButtons.SetActive(false);
        StartCoroutine(nameof(LoadLevelAsync), sceneIndex);
        
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

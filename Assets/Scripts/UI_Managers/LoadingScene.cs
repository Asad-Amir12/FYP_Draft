using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] Image loadingFill;
    [SerializeField] private GameObject mainMenuButtons;

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
        
    }
    public void LoadScene(int sceneIndex)
    {
        //gameObject.SetActive(true);
        loadingScreen.SetActive(true);
        mainMenuButtons.SetActive(false);
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

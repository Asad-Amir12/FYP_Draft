using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{


    [SerializeField] Image loadingFill;
    [SerializeField] GameObject loadingCanvas;
    //[SerializeField] private GameObject mainMenuButtons;

    public static event Action OnGameSceneLoaded;
    public static event Action OnReturnToMainMenu;

    public static LoadingScene Instance;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

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
        loadingCanvas.SetActive(false);
        if (sceneIndex == 2)
        {

        }
        if (sceneIndex == 1)
        {
            OnGameSceneLoaded?.Invoke();
            SoundManager.Instance.PlaySoundWithFade(SoundData.GameBGM, true);
        }
        if (SceneManager.GetActiveScene().buildIndex == 0 && sceneIndex == 0)
        {
            OnReturnToMainMenu?.Invoke();
        }

    }
    public void LoadScene(int sceneIndex)
    {
        //gameObject.SetActive(true);
        loadingCanvas.SetActive(true);
        // mainMenuButtons.SetActive(false);
        SoundManager.Instance.StopSoundWithFade(5f);
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

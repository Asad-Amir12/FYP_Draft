using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    public static MainMenuUI Instance;

    public GameObject menu;
    private InputReader inputReader;

    private bool GamePaused = false;
    [SerializeField] private GameObject inGameMenu;





    private void Awake()
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

        LoadingScene.OnGameSceneLoaded += OnGameSceneLoaded;
        LoadingScene.OnReturnToMainMenu += OnReturnToMainMenu;
        //EventBus.GameResumed += OnMenuControlsPerformed;

    }
    void Start()
    {
        SoundManager.Instance.PlaySoundWithFade(SoundData.MenuBGM, true);
    }

    public void LoadGameScene(int index)
    {
        LoadingScene.Instance.LoadScene(index);
    }

    public void OnGameSceneLoaded()
    {
        inputReader = FindObjectOfType<InputReader>();

        // inGameMenuParent =  new();
        //  inGameMenuParent.SetActive(false);
        if (menu)
        {
            Destroy(menu);
            menu = null;
        }
        menu = Instantiate(inGameMenu);

        // ? Making sure it is off
        menu.SetActive(false);
        if (inputReader != null)
        {
            inputReader.OnMenuControlsPerformed += OnMenuControlsPerformed;
        }
    }

    public void OnMenuControlsPerformed()
    {

        menu.SetActive(!menu.activeSelf);
        GamePaused = menu.activeSelf;
        DataCarrier.isGamePaused = GamePaused;
        inputReader.GamePaused = GamePaused;

        if (GamePaused)
        {
            inputReader.DisableControls();
            Time.timeScale = 0;
        }
        else
        {
            inputReader.EnableControls();
            Time.timeScale = 1;
        }
    }

    public void ResumeGameForcefully()
    {
        if (menu == null)
        {
            return;
        }
        menu.SetActive(false);
        GamePaused = false;
        DataCarrier.isGamePaused = false;
        inputReader.GamePaused = false;


        inputReader.EnableControls();
        Time.timeScale = 1;

    }

    public void OnReturnToMainMenu()
    {
        // EventBus.GameResumed -= OnMenuControlsPerformed;
        if (menu != null)

            Destroy(menu);

        if (inputReader != null)
            inputReader.OnMenuControlsPerformed -= OnMenuControlsPerformed;
    }





    public void Quit()
    {

        Application.Quit();

    }

}

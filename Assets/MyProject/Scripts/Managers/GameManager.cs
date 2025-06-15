using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private float elapsedTime;
    private bool isGameStarted = false;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private TextMeshProUGUI timerText;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
        LevelGenerator.Instance.OnLevelGenerated += OnLevelGenerated;
        EventBus.OnLevelCleared += OnLevelCleared;
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    void OnLevelGenerated()
    {

        // Initialize player stats or other game elements here
        Debug.Log("Level generated, initializing player stats.");
        StartGame();

    }

    IEnumerator LevelTimer()
    {
        while (isGameStarted)
        {
            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
            timerText.text = "Time: " + elapsedTime.ToString();

        }
    }
    public void ResetTimer()
    {
        elapsedTime = 0f;
    }
    public void StartTimer()
    {
        StartCoroutine(LevelTimer());
    }

    public void StartGame()
    {
        isGameStarted = true;
        ResetTimer();
        StartCoroutine(LevelTimer());
        HUDUI.Instance.ToggleHUD(true);
    }
    public void StopGame()
    {
        isGameStarted = false;
        StopCoroutine(LevelTimer());
        HUDUI.Instance.ToggleHUD(false);
        //HUDUI.Instance.gameObject.SetActive(false);

    }
    public void OnLevelCleared()
    {
        StopGame();
        DataCarrier.SelectedLevelIndex++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

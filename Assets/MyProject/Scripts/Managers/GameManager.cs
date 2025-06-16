using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private float elapsedTime;
    private bool isGameStarted = false;
    // [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private TextMeshProUGUI timerText;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        EventBus.OnLevelGenerated += OnLevelGenerated;
        EventBus.OnLevelCleared += OnLevelCleared;
        EventBus.OnLevelFailed += OnLevelFailed;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnDestroy()
    {
        EventBus.OnLevelGenerated -= OnLevelGenerated;
        EventBus.OnLevelCleared -= OnLevelCleared;
        EventBus.OnLevelFailed -= OnLevelFailed;
    }
    void OnLevelGenerated()
    {
        if (timerText == null)
        {
            timerText = HUDUI.Instance.TimerObject.GetComponentInChildren<TextMeshProUGUI>();
        }

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
        StopAllCoroutines();
        ResetTimer();
        isGameStarted = true;
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
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnLevelFailed()
    {
        StopGame();

        DataCarrier.ResetData();
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.ResetCurrency();
        }
        if (HUDUI.Instance != null)
        {
            HUDUI.Instance.UpdateCurrency();
        }
    }
    void OnDisable()
    {
        if (LevelGenerator.Instance != null)
            EventBus.OnLevelGenerated -= OnLevelGenerated;
        EventBus.OnLevelCleared -= OnLevelCleared;
        EventBus.OnLevelFailed -= OnLevelFailed;
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    }

}

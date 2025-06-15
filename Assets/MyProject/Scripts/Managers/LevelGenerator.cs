using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Purchasing.MiniJSON;
public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance;
    //public event Action OnLevelGenerated;
    private LevelData currentLevelData;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        currentLevelData = JsonConvert.DeserializeObject<LevelData>(LevelDataHolder.Instance.levelsFile.LevelFiles[DataCarrier.SelectedLevelIndex].text);
        string pretty = JsonConvert.SerializeObject(
                                                    currentLevelData,
                                                    Formatting.Indented
                                                    );
        Debug.Log(pretty);
        // Level generation logic goes here
        Debug.Log("Level generated!");
        EventBus.TriggerOnLevelGenerated();
        EnemySpawner.Instance.StartSpawner(currentLevelData);

    }

}

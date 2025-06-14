using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataHolder : MonoBehaviour
{
    public static LevelDataHolder Instance { get; private set; }
    public LevelsFile levelsFile;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }
}

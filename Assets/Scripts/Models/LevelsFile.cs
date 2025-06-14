using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "Levels")]
public class LevelsFile : ScriptableObject
{
    public List<TextAsset> LevelFiles;
}

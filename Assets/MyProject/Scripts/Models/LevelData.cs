using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{

    public List<EnemyData> enemyData;
    public float timeBetweenSpawns;
    public int concurrentEnemyCount;

}
[System.Serializable]
public enum EnemyType
{
    Creep, Demon, BallDemon
}


[System.Serializable]

public class EnemyData
{
    public EnemyType enemyType;
    public int enemyCount;
    public int maxHealth;
    public float invincibilityDuration;


    public int baseDamage;
    public float defense;
    public float movementMultiplier;
}

using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelModel 
{
    public int LevelID;
    public string LevelName;

    public LevelType LevelType;
    public Transform[] SpawnPositions;

    public int startEnergy;
    public int startMoney;

    public int actualLevelScore = 0;
    public int highScore = 0;
    public Action OnScore;

    [HideInInspector]public int ActualLevel;
    [HideInInspector]public int WaveIndex;
    public List<Wave> Waves = new List<Wave>();

    public Action OnNextWave;
}

public enum LevelType
{
    Easy,
    Normal,
    Expert,
    Hardcore
}


[System.Serializable]
public class Wave
{
    public List<WaveData> WaveData = new List<WaveData>();
}

[System.Serializable]
public struct WaveData
{
    public EnemyController EnemyBehaviour;
    public int amount;

    public float spawnTime;
    public float initialTime;
}
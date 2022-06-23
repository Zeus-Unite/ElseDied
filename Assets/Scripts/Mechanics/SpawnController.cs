using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    public static SpawnController Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    internal void StartWave()
    {
        SpawnRunning = true;
        StartCoroutine("HandleWave");
    }

    void OnDisable()
    {
        if (Instance == this) Instance = null;
    }

    public bool SpawnRunning = false;
    public bool SpawnControlActive = false;
    public Action<int> RemainingEnemies;

    readonly LevelModel levelmodel = Simulation.GetModel<LevelModel>();

    List<EnemyController> instantiatedEnemeies = new List<EnemyController>();


    public void RemoveEnemyFromList(EnemyController ec)
    {
        lock (instantiatedEnemeies)
        {
            instantiatedEnemeies.Remove(ec);
        }
    }

    IEnumerator HandleWave()
    {
        WaitForFixedUpdate fixedWait= new WaitForFixedUpdate();

        int waveDataCount = levelmodel.Waves[levelmodel.WaveIndex-1].WaveData.Count;
        float[] waveTime = new float[waveDataCount];
        float[] initialTime = new float[waveDataCount];
        int[] spawnAmount = new int[waveDataCount];

        for (int i = 0; i < levelmodel.Waves[levelmodel.WaveIndex-1].WaveData.Count; i++)
        {
            waveTime[i] = levelmodel.Waves[levelmodel.WaveIndex-1].WaveData[i].spawnTime;
            initialTime[i] = levelmodel.Waves[levelmodel.WaveIndex-1].WaveData[i].initialTime;
            spawnAmount[i] = levelmodel.Waves[levelmodel.WaveIndex-1].WaveData[i].amount;
        }


        while (SpawnRunning)
        {
            for (int i = 0; i < waveTime.Length; i++)
            {
                if (spawnAmount[i] <= 0 || initialTime[i] > 0)
                {
                    initialTime[i] -= Time.fixedDeltaTime;
                    continue;
                }

                waveTime[i] -= Time.fixedDeltaTime;

                if (waveTime[i] < 0)
                {
                    EnemyController ec = Instantiate(levelmodel.Waves[levelmodel.WaveIndex-1].WaveData[i].EnemyBehaviour, levelmodel.SpawnPositions[Random.Range(0, levelmodel.SpawnPositions.Length)].position, Quaternion.identity);
                    instantiatedEnemeies.Add(ec);

                    waveTime[i] = levelmodel.Waves[levelmodel.WaveIndex-1].WaveData[i].spawnTime;
                    spawnAmount[i]--;
                }

            }

            for (int z = 0; z < spawnAmount.Length; z++)
            {
                if (spawnAmount[z] > 0)
                    break;

                SpawnRunning = false;
            }
            yield return fixedWait;
        }


        while(instantiatedEnemeies.Count > 0)
        {
            RemainingEnemies(instantiatedEnemeies.Count);
            yield return fixedWait;
        }

        RemainingEnemies(0);
        Simulation.Schedule<EndWave>();
        yield return null;
    }

    public void ClearPlayField()
    {
        StopAllCoroutines();

        for (int i = 0; i < instantiatedEnemeies.Count; i++)
        {
            Destroy(instantiatedEnemeies[i].gameObject);
        }

        instantiatedEnemeies.Clear();
    }

}

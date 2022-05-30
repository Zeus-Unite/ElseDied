using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] float randomPickUpSpawnTime;

    [SerializeField] Vector3 spawnAreaA;
    [SerializeField] Vector3 spawnAreaB;

    float nextSpawnTime = 0;

    readonly GameModel gamemodel = Simulation.GetModel<GameModel>();

    void Update()
    {
        if (SpawnController.Instance.SpawnControlActive)
        {
            nextSpawnTime += Time.deltaTime;
            if (nextSpawnTime < randomPickUpSpawnTime)
                return;
            
            Vector3 rndPosWithin;
            rndPosWithin = new Vector3(Random.Range(-30f, 30f), Random.Range(-2f, 18f), 0);
            Instantiate(gamemodel.pickUpPrefabs[Random.Range(0, gamemodel.pickUpPrefabs.Count)], rndPosWithin, Quaternion.identity);
            nextSpawnTime = 0;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(spawnAreaB, spawnAreaA);
        Gizmos.DrawLine(spawnAreaA, spawnAreaB);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> wavesConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping = true;
    WaveConfigSO currentWave;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());        
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }    
    
    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (WaveConfigSO wave in wavesConfigs)
            {
                currentWave = wave;

                for (int i = 0; i < currentWave.GetSpwanObjectsCount(); i++)
                {
                    Instantiate(currentWave.GetSpawnObjectPrefab(i), currentWave.GetStartingWaypoint().position, Quaternion.Euler(0,0,180), transform);

                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return timeBetweenWaves;
            }
        }
        while (isLooping);

    }


}

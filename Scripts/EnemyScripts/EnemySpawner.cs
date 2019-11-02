using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;                           
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());

        } while (looping);
        
    }

    IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
             var currentWave = waveConfigs[waveIndex];

            yield return StartCoroutine(SpawnAllEnemysInWave(currentWave));
        }
    }

   IEnumerator SpawnAllEnemysInWave(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetNumberOfEnemys(); i++)
        {

           var newEnemy =  Instantiate(
               waveConfig.GetEnemyPrefab(), 
               waveConfig.GetWaypoints()[0].transform.position,
               waveConfig.GetEnemyPrefab().transform.rotation);
                       
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
        
    }

   
}

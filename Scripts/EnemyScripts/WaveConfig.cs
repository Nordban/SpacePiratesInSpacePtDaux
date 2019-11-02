using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] float timeBetweenSpwans = 1f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemys = 5;
    [SerializeField] float moveSpeed = 2f;


    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;

    public GameObject GetEnemyPrefab() { return enemyPrefab;}
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();

        foreach(Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        
        return waveWaypoints;
    }
    public float GetTimeBetweenSpawns() { return timeBetweenSpwans; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public int GetNumberOfEnemys() { return numberOfEnemys; }
    public float GetMoveSpeed() { return moveSpeed; }

}

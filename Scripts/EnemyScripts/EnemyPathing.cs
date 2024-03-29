﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;    
    int waypointIndex = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        
        waypoints = waveConfig.GetWaypoints();
        Debug.Log(waypoints);
        transform.position = waypoints[0].transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {        
        if (waypointIndex <= waypoints.Count - 1)
        {
            
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
                       
            if (transform.position == targetPosition)
            {               
                waypointIndex++;              
            }

        }
        else
        {
            Destroy(gameObject);
        }
    }
}

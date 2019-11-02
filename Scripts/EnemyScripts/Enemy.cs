using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioSource normalEnemyLaser;
    //public AudioSource explosionSFX;
    [SerializeField] float health = 100;
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float laserSpeed;
    [SerializeField] GameObject deathPlaceHolder;
    Animation death;
    

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndFire();
    }

    private void CountDownAndFire()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject enemyLaser = Instantiate(enemyLaserPrefab, transform.position, enemyLaserPrefab.transform.rotation) as GameObject;
        normalEnemyLaser.Play();
        enemyLaser.GetComponent<Rigidbody>().velocity = new Vector3(0, -laserSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }
            health -= damageDealer.GetDamage();
            damageDealer.Hit();
            ProcessHit(damageDealer);
        }

    }
    private void ProcessHit(DamageDealer damageDealer)
    {
       

        if (health <= 0)
        {
            GameObject explosion = Instantiate(deathPlaceHolder, transform.position, deathPlaceHolder.transform.rotation);
            
            Destroy(gameObject);
            
        }

    }
}

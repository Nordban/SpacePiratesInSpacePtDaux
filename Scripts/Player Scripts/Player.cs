using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 6.75f;
    [SerializeField] float fireRate = 1f;
    [SerializeField] int playerHealth = 1000;
    [SerializeField] GameObject deathAnimation;
    public AudioSource playerLaser;
    //public AudioSource explosionSFX;

    [Header("Weapons")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] GameObject laserSpawnRight;
    [SerializeField] GameObject laserSpawnLeft;
    [SerializeField] GameObject missileSpawnRight;
    [SerializeField] GameObject missileSpawnLeft;
    [SerializeField] float laserSpeed = 10f;
    
    //[SerializeField] float MissileSpeed = 10f;



    Coroutine ceaseFire;

    int laserCounter = 0;
    int missileCounter = 0;
    int missileCount;
    // Horizontal screen boundaries
    float xMin;
    float xMax;
    // Vertical screen boundries
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        MoveBoundaries();
        missileCount = 6;
    }



    // Update is called once per frame
    void Update()
    {
        Move();
        FireLasers();
        FireMissile();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer){ return; }
            playerHealth -= damageDealer.GetDamage();
            damageDealer.Hit();
            ProcessHit(damageDealer);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        if (playerHealth <= 0)
        {
            GameObject death = Instantiate(deathAnimation, transform.position, Quaternion.identity);
            //explosionSFX.Play();
            Destroy(gameObject);
        }
    }

    private void FireLasers()
    {   //Fire
        if (Input.GetButtonDown("Fire1"))
        {
            ceaseFire = StartCoroutine(AutoFire());
        }
        //Cease Fire
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(ceaseFire);
        }
    }


    IEnumerator AutoFire()
    {
        while (true)
        {
            //Fire RIGHT Laser
            while (laserCounter == 0)
            {
                GameObject rightLaser = Instantiate(laserPrefab,
                    laserSpawnRight.transform.position,
                    Quaternion.identity) as GameObject;
                playerLaser.Play();
                rightLaser.GetComponent<Rigidbody>().velocity = new Vector3(0, laserSpeed, 0);
                laserCounter = 1;
                yield return new WaitForSeconds(fireRate);
            }

            //Fire LEFT Laser
            while (laserCounter == 1)
            {
                GameObject leftLaser = Instantiate(laserPrefab,
                   laserSpawnLeft.transform.position,
                   Quaternion.identity) as GameObject;
                playerLaser.Play();
                leftLaser.GetComponent<Rigidbody>().velocity = new Vector3(0, laserSpeed, 0);
                laserCounter = 0;
                yield return new WaitForSeconds(fireRate);
            }
        }
    }


    private void FireMissile()
    {
        if (missileCount != 0)
        {
            //Fire RIGHT Missile
            if (Input.GetButtonDown("Fire2") && missileCounter == 0 && missileCount > 0)
            {
                GameObject rightMissile = Instantiate(missilePrefab,
                    missileSpawnRight.transform.position,
                    missilePrefab.transform.rotation) as GameObject;

                rightMissile.GetComponent<Rigidbody>().velocity = new Vector3(0, 5f, 0);
                missileCounter = 1;
                missileCount--;
            }
            //Fire LEFT Missile
            else if (Input.GetButtonDown("Fire2") && missileCounter == 1 && missileCount > 0)
            {
                GameObject leftMissile = Instantiate(missilePrefab,
                    missileSpawnLeft.transform.position,
                    missilePrefab.transform.rotation) as GameObject;

                leftMissile.GetComponent<Rigidbody>().velocity = new Vector3(0, 5f, 0);

                missileCounter = 0;
                missileCount--;
            }
        }


    }

    private void Move()
    {
        //Horizontal Movement
        var deltaX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin + .7f, xMax - .7f);

        //Vertical Movment
        var deltaY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin + .7f, yMax - 1.2f);

        //Horizontal Movement
        //var deltaX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        //var newXPos = transform.position.x + deltaX;

        ////Vertical Movment
        //var deltaY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        //var newYPos = transform.position.y + deltaY;

        transform.position = new Vector2(newXPos, newYPos);

    }
    private void MoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)).y;

    }
}

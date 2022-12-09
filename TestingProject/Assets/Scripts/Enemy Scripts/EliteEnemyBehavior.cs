using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEnemyBehavior : MonoBehaviour
{

    public Transform shootingPointLeft;
    public Transform shootingPointRight;
    public GameObject bulletPrefab;
    public Transform eliteEnemy;

    public double doubleHealth = 10d;
    private Vector3 spawnPosition;
    public float fltBulletFireRate;
    private float fltMoveSpeed = 8f;

    private float fltTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        doubleHealth = 10d;
        spawnPosition = gameObject.transform.position;
        spawnPosition.y = spawnPosition.y - 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position != spawnPosition)     // If not at starting location, move there
        {
            Vector3 newPos = Vector3.MoveTowards(gameObject.transform.position, spawnPosition, fltMoveSpeed * Time.deltaTime);
            gameObject.transform.position = newPos;
        }
        if (gameObject.transform.position == spawnPosition) bulletBehavior(fltBulletFireRate);   // If at starting location, begin shooting
    }

    // When a bullet enters the collision box of the elite enemy, take damage
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            takeDamage(other, 1);
        } else if(other.gameObject.tag == "UpgradedBullet")
        {
            takeDamage(other, 1.2);
        }
    }

    // Function that handles health and damage calculations
    public void takeDamage(Collider2D other, double doubleDamageTaken)
    {
        Destroy(other.gameObject);
        doubleHealth = doubleHealth - doubleDamageTaken;
        if (doubleHealth <= 0)
        {
            ScoreManager.instance.AddPoint();
            Destroy(gameObject);
        }
    }

    // Function that handles bullet shooting
    void bulletBehavior(float fltFireRate)
    {
        if (Time.time >= fltTimer)
        {
            Instantiate(bulletPrefab, shootingPointLeft.position, transform.rotation, eliteEnemy);
            Instantiate(bulletPrefab, shootingPointRight.position, transform.rotation, eliteEnemy);
            fltTimer = Time.time + fltFireRate;
        }
    }
}

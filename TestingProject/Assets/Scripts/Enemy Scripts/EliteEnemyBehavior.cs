using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEnemyBehavior : MonoBehaviour
{

    public Transform shootingPointLeft;
    public Transform shootingPointRight;
    public GameObject bulletPrefab;
    public Transform eliteEnemy;

    public int intHealth;
    private Vector3 spawnPosition;
    public float fltBulletFireRate;
    private float fltMoveSpeed = 2f;

    private float fltTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bulletBehavior(fltBulletFireRate);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            takeDamage(other, 1);
        }
    }

    public void takeDamage(Collider2D other, int intDamageTaken)
    {
        Destroy(other.gameObject);
        intHealth = intHealth - intDamageTaken;
        if (intHealth <= 0)
        {
            ScoreManager.instance.AddPoint();
            Destroy(gameObject);
        }
    }

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

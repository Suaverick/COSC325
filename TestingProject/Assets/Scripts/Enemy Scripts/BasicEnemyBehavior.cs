using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBehavior : MonoBehaviour
{

    // Variables needed for basic enemy
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public Transform basicEnemy;
    private Vector3 spawnPosition;
    public float fltBulletFireRate;
    private float fltMoveSpeed = 8f;

    private float fltTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = gameObject.transform.position;
        spawnPosition.y = spawnPosition.y - 8;
    }

    // Update is called once per frame
    void Update()
    {
        // Move to correct starting position
        if (gameObject.transform.position != spawnPosition)     // If current position does not equal its spawn position
        {
            Vector3 newPos = Vector3.MoveTowards(gameObject.transform.position, spawnPosition, fltMoveSpeed * Time.deltaTime);
            gameObject.transform.position = newPos;
        }
        if (gameObject.transform.position == spawnPosition) bulletBehavior(fltBulletFireRate);     // If current position does equal spawn position, shoot bullets
    }

    // When a trigger enters the collision box of the enemy
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "UpgradedBullet" )
        {
            ScoreManager.instance.AddPoint();
            SwapBar.instance.IncrementProgress(10f);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
    
    // Function that handles the shooting of the basic enemy
    void bulletBehavior(float fltFireRate)
    {
        if (Time.time >= fltTimer)
        {
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation, basicEnemy);
            fltTimer = Time.time + fltFireRate;
        }
    }

}

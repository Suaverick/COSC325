using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBehavior : MonoBehaviour
{

    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public Transform basicEnemy;
    public float fltBulletFireRate;

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
            ScoreManager.instance.AddPoint();
            Destroy(other.gameObject);
            Destroy(gameObject);

        }
    }

    void bulletBehavior(float fltFireRate)
    {
        if (Time.time >= fltTimer)
        {
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation, basicEnemy);
            fltTimer = Time.time + fltFireRate;
        }
    }

}

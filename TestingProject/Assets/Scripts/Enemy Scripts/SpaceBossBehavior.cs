using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBossBehavior : MonoBehaviour
{

    public GameObject misslePrefab;
    public Transform shootingPoint1;
    public Transform shootingPoint2;
    public Transform shootingPoint3;
    public Transform shootingPoint4;
    public int intMisslesShot;

    public float fltBulletFireRate;
    private float fltMoveSpeed = 2f;
    private float fltTimer = 0;
    private float fltWaitTimer = 0;

    public int intHealth = 100;

    private bool boolPhase1 = false;
    private bool boolPhase2 = false;
    private bool boolPhase3 = false;
    private bool boolWaitOn = false;

    // Start is called before the first frame update
    void Start()
    {
        boolPhase1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        shootMissles(fltBulletFireRate);
    }

    void shootMissles(float fltFireRate)
    {
        if (Time.time >= fltTimer && intMisslesShot <= 3)
        {
            Instantiate(misslePrefab, shootingPoint1.position, transform.rotation, shootingPoint1);
            Instantiate(misslePrefab, shootingPoint2.position, transform.rotation, shootingPoint2);
            Instantiate(misslePrefab, shootingPoint3.position, transform.rotation, shootingPoint3);
            Instantiate(misslePrefab, shootingPoint4.position, transform.rotation, shootingPoint4);
            intMisslesShot++;
            fltTimer = Time.time + fltFireRate;
        }
        else if (Time.time <= fltTimer && intMisslesShot <= 3)
        {

        }
        else
        {
            waitBetweenShots();
        }
    }

    void waitBetweenShots()
    {
        if (!boolWaitOn)
        {
            fltWaitTimer = Time.time + 3f;
            boolWaitOn = true;
        }
        if (Time.time >= fltWaitTimer)
        {
            intMisslesShot = 0;
            boolWaitOn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {
            takeDamage(other, 1);
        }
    }

    public void takeDamage(Collider2D other, int intDamage)
    {

    }
}

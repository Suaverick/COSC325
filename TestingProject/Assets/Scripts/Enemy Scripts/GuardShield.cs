using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardShield : MonoBehaviour
{
    public double doubleHealth = 10;

    private float fltTimer;
    public float fltOnline;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.localScale == new Vector3(0, 0, 0))   // If the current size of the sheild is 0, bring shield back up
        {
            shieldsOnline();
        }
    }

    // When a bullet enters the collision box of the guard shield
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

    // Function that handles damage and health calculations
    public void takeDamage(Collider2D other, double doubleDamageTaken)
    {
        Destroy(other.gameObject);
        doubleHealth = doubleHealth- doubleDamageTaken;
        if (doubleHealth <= 0)
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            fltTimer = Time.time + fltOnline;
        }
    }

    // Turns the shield back on after a specified amoount of time has passed (fltOnline)
    public void shieldsOnline()
    {
        if (Time.time >= fltTimer)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            doubleHealth = 10;
        }
    }

}

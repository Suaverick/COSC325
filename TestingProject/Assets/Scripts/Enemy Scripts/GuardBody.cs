using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBody : MonoBehaviour
{

    public double doubleHealth = 10d;

    // Start is called before the first frame update
    void Start()
    {
        doubleHealth = 10d;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            takeDamage(1);
            Destroy(other.gameObject);
        } 
        if(other.gameObject.tag == "UpgradedBullet")
        {
            takeDamage(1.2);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "SpaceBossWave")
        {
            takeDamage(200);
        }
    }

    public void takeDamage(double doubleDamageTaken)
    {
        doubleHealth = doubleHealth - doubleDamageTaken;
        if (doubleHealth <= 0)
        {
            ScoreManager.instance.AddPoint();
            gameObject.SetActive(false);
        }
    }

}

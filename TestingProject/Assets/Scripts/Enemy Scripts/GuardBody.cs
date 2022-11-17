using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBody : MonoBehaviour
{

    public int intHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            takeDamage(1);
        }
        if (other.gameObject.tag == "SpaceBossWave")
        {
            takeDamage(200);
        }
    }

    public void takeDamage(int intDamageTaken)
    {
        intHealth = intHealth - intDamageTaken;
        if (intHealth <= 0)
        {
            ScoreManager.instance.AddPoint();
            gameObject.SetActive(false);
        }
    }

}

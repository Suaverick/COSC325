using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomberEnemyBehavior : MonoBehaviour
{

    private GameObject player;
    public float fltSpeed;
    public float fltSuicideSpeed;
    public float fltCountdown;
    public int intHealth;

    private float fltTimer;
    private Vector2 playerLocation;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        fltTimer = Time.time + fltCountdown;
        player = GameObject.FindGameObjectWithTag("Player");
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.activeInHierarchy) fltTimer = Time.time + fltCountdown;
        if (Time.time <= fltTimer)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), fltSpeed * Time.deltaTime);
            playerLocation = player.transform.position;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerLocation.x, playerLocation.y - 25), fltSuicideSpeed * Time.deltaTime);
            if (transform.position.y < -screenBounds.y)
            {
                Destroy(this.gameObject);
            }
        }
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
        if(intHealth <= 0)
        {
            ScoreManager.instance.AddPoint();
            Destroy(gameObject);
        }
    }

}

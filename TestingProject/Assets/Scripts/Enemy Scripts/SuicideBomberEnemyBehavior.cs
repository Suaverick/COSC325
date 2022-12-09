using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomberEnemyBehavior : MonoBehaviour
{

    private GameObject player;
    private Renderer rend;
    public float fltSpeed;
    public float fltSuicideSpeed;
    public float fltCountdown;
    public double doubleHealth;

    private float fltTimer;
    public float fltMoveSpeed = 8f;

    private Vector2 playerLocation;
    private Vector2 screenBounds;
    private Vector3 spawnPosition;

    private bool boolAtSpawn = false;

    [SerializeField]
    private Color colorToTurnTo = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = gameObject.transform.position;
        spawnPosition.y = spawnPosition.y - 8;
        player = GameObject.FindGameObjectWithTag("Player");
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!boolAtSpawn) 
        {
            if (gameObject.transform.position != spawnPosition)
            {
                Vector3 newPos = Vector3.MoveTowards(gameObject.transform.position, spawnPosition, fltMoveSpeed * Time.deltaTime);
                gameObject.transform.position = newPos;
            }
            if (gameObject.transform.position == spawnPosition)
            {
                fltTimer = Time.time + fltCountdown;
                boolAtSpawn = true;
            }
        }
        else if (boolAtSpawn)
        {
            if (Time.time <= fltTimer)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), fltSpeed * Time.deltaTime);
                playerLocation = player.transform.position;
            }
            else
            {
                rend.material.color = colorToTurnTo;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerLocation.x, playerLocation.y - 25), fltSuicideSpeed * Time.deltaTime);
                if (transform.position.y < -screenBounds.y)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

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

    public void takeDamage(Collider2D other, double doubleDamageTaken)
    {
        Destroy(other.gameObject);
        doubleHealth = doubleHealth - doubleDamageTaken;
        if(doubleHealth <= 0)
        {
            ScoreManager.instance.AddPoint();
            Destroy(gameObject);
        }
    }

}

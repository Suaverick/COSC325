using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEnemyBehavior : MonoBehaviour
{

    public GameObject guard;
    public GameObject follow;

    private Vector3 spawnPosition;

    public float fltSpeed = 8;
    private float fltMoveSpeed = 8f;

    private bool boolSwitch = false;
    private bool boolAtPosition = false;

    private Vector2 enemyLocation;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = gameObject.transform.position;
        spawnPosition.y = spawnPosition.y - 8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position != spawnPosition && !boolAtPosition)     // If enemy is not at its starting location, move there
        {
            Vector3 newPos = Vector3.MoveTowards(gameObject.transform.position, spawnPosition, fltMoveSpeed * Time.deltaTime);
            gameObject.transform.position = newPos;
        }
        if(gameObject.transform.position == spawnPosition)                         // If at starting location, turn off movement
        {
            if(!boolSwitch)
            {
                boolSwitch = true;
                boolAtPosition = true;
            }
        }

        if (!guard.activeInHierarchy)          // If the guard body is not active in the guard object, destroy the guard object
        {
            Destroy(gameObject);
        }
        if (follow != null && boolAtPosition)      // If the guard is following another enemy and is at its starting location, follow the x-axis of the enemy
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(follow.transform.position.x, transform.position.y), fltSpeed * Time.deltaTime);
            enemyLocation = follow.transform.position;
        }
    }

}

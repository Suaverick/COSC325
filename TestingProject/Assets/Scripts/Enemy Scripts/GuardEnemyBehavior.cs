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

    private Vector2 enemyLocation;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = gameObject.transform.position;
        spawnPosition.y = spawnPosition.y - 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position != spawnPosition)
        {
            Vector3 newPos = Vector3.MoveTowards(gameObject.transform.position, spawnPosition, fltMoveSpeed * Time.deltaTime);
            gameObject.transform.position = newPos;
        }
        if (!guard.activeInHierarchy)
        {
            Destroy(gameObject);
        }
        if (follow != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(follow.transform.position.x, transform.position.y), fltSpeed * Time.deltaTime);
            enemyLocation = follow.transform.position;
        }
    }

}

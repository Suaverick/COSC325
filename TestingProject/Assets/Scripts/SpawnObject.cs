using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{

    public GameObject basicEnemy;
    GameObject go;
    public Transform spawner;
    Vector3 spawnPosition;
    float fltMoveSpeed = 2f;

    public bool boolIsEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        if (boolIsEnabled == true)
        {
            SpawnBasicEnemy(0, 3);
            SpawnBasicEnemy(-2, 3);
            SpawnBasicEnemy(2, 3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if(go.transform.position != spawnPosition)
        {
            Vector3 newPos = Vector3.MoveTowards(go.transform.position, spawnPosition, fltMoveSpeed * Time.deltaTime);
            go.transform.position = newPos;
        }*/
    }

    void SpawnBasicEnemy(float spawnPositionX, float spawnPositionY)
    {
        spawnPosition.x = spawnPositionX;
        spawnPosition.y = spawnPositionY;
        spawnPosition.z = 0;

        Vector3 offScreenSpawn;
        offScreenSpawn = spawnPosition;
        offScreenSpawn.y = spawnPosition.y + 8;

        go = Instantiate(basicEnemy, spawnPosition, basicEnemy.transform.rotation, spawner);

    }
}

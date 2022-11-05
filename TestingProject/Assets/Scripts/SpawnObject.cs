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
       
    }

    void SpawnBasicEnemy(float spawnPositionX, float spawnPositionY)
    {

        spawnPosition.x = spawnPositionX;
        spawnPosition.y = spawnPositionY;
        spawnPosition.z = 0;

        Vector3 offScreenSpawn;
        offScreenSpawn = spawnPosition;
        offScreenSpawn.y = spawnPosition.y + 8;

        Instantiate(basicEnemy, offScreenSpawn, basicEnemy.transform.rotation, spawner);

    }
}

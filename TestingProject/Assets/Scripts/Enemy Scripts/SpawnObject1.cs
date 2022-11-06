using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject1 : MonoBehaviour
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
            SpawnBasicEnemy(0, 3, true);
            SpawnBasicEnemy(2, 3, true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnBasicEnemy(float spawnPositionX, float spawnPositionY, bool boolIsLeft)
    {
        spawnPosition.x = spawnPositionX;
        spawnPosition.y = spawnPositionY;
        spawnPosition.z = 0;

        Vector3 offScreenSpawn;
        offScreenSpawn = spawnPosition;
        offScreenSpawn.y = spawnPosition.y + 8;

        go = Instantiate(basicEnemy, offScreenSpawn, basicEnemy.transform.rotation, spawner);

    }
}

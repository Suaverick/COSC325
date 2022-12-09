using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedBossMatter : MonoBehaviour
{   
    public float rotationSpeed;
    private Vector2 direction;
    public GameObject player;
    public Transform tentacle;

    private GameObject go;

    public Transform shootingPoint;
    private float fltTimer = 0;
    public GameObject bulletPrefab;
    public float fltBulletFireRate;

    public bool boolStretch;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Rotates matter to track the player and shoot bullets in the direction of the player
    // Update is called once per frame
    void Update()
    {
        direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        bulletBehavior(fltBulletFireRate);
    }

    // Function for handling bullet spawning
    void bulletBehavior(float fltFireRate)
    {
        if (Time.time >= fltTimer)
        {
            go = Instantiate(bulletPrefab, shootingPoint.position, transform.rotation, gameObject.transform);     // object is spawned as a child for the locking mechanism
            go.transform.localScale = new Vector3(go.transform.localScale.x * 3, go.transform.localScale.y * 3, go.transform.localScale.z * 3);
            fltTimer = Time.time + fltFireRate;
        }
    }
}

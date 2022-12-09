using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaclesTest : MonoBehaviour
{

    // Originally for tentacles, now for the basic boss matter

    public float rotationSpeed;
    private Vector2 direction;
    public GameObject player;
    public Transform tentacle;

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

    // Rotate and track player and shoot bullets in the direction of the player
    // Update is called once per frame
    void Update()
    {
        direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        bulletBehavior(fltBulletFireRate);
    }

    // Shoot bullets
    void bulletBehavior(float fltFireRate)
    {
        if (Time.time >= fltTimer)
        {
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
            fltTimer = Time.time + fltFireRate;
        }
    }
}

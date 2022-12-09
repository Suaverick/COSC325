using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpinner : MonoBehaviour
{

    private float fltTargetScale = 0.3f;

    public Transform shootingPoint;
    private float fltTimer = 0;
    public GameObject bulletPrefab;
    public float fltBulletFireRate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.localScale.y <= fltTargetScale)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, transform.localScale.y + .005f, transform.localScale.z);
        }
        else {
            bulletBehavior(fltBulletFireRate);
        }
    }

    void bulletBehavior(float fltFireRate)
    {
        if (Time.time >= fltTimer)
        {
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
            fltTimer = Time.time + fltFireRate;
        }
    }
}

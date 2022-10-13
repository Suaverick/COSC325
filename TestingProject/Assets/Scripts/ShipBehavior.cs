using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehavior : MonoBehaviour {

    public float fltMinX;
    public float fltMaxX;
    public float fltMinY;
    public float fltMaxY;

    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public float fltBulletFireRate;

    float fltTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        ShipPosition();   
    }

    void ShipPosition() {

        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                bulletBehavior(fltBulletFireRate);
            }
            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector2(touchPosition.x, touchPosition.y);
                bulletBehavior(fltBulletFireRate);
            }
            if (touch.phase == TouchPhase.Ended)
            {

            }


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

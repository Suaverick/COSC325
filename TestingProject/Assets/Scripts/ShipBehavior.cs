using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehavior : MonoBehaviour {

    public float fltMinX;
    public float fltMaxX;
    public float fltMinY;
    public float fltMaxY;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float fltBulletSpeed;

    Collider2D col;

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

            }
            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector2(touchPosition.x, touchPosition.y);
                bulletBehavoir();

            }
            if (touch.phase == TouchPhase.Ended)
            {

            }


        }

    }

    void bulletBehavoir()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * fltBulletSpeed;
    }

}

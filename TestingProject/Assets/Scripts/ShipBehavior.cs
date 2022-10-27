using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipBehavior : MonoBehaviour {

    public float fltMinX;
    public float fltMaxX;
    public float fltMinY;
    public float fltMaxY;

    public static ShipBehavior Instance;

    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public float fltBulletFireRate;

    private bool boolIsLeft = true;

    float fltTimer = 0;
    float fltSwapTimer = 0;

    // Start is called before the first frame update
    void Start()
    {

        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);

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
            if (touch.phase == TouchPhase.Stationary)
            {
                bulletBehavior(fltBulletFireRate);
            }
            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector2(touchPosition.x, touchPosition.y);
                bulletBehavior(fltBulletFireRate);
            }
            if (touch.phase == TouchPhase.Ended)
            {
                //ScreenSwap();
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

    void ScreenSwap()
    {
        if (Time.time >= fltSwapTimer)
        {
            if (transform.position.x >= 2 && boolIsLeft == true)
            {
                SceneManager.LoadScene("SwappedLevel");
                boolIsLeft = false;
                fltSwapTimer = Time.time + 1;
            }
            /*if (transform.position.x >= -2 && boolIsLeft == false)
            {
                SceneManager.LoadScene("TestLevel");
                boolIsLeft = true;
                fltSwapTimer = Time.time + 1;
            }*/
        }
    }

}

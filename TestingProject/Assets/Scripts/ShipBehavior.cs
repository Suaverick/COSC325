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

    float fltTimer = 0;

    // Start is called before the first frame update
    void Start()
    {

        // If this object already exists, destroy all duplicates
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }


        Instance = this;

        // Does not destroy object when moving between scenes
        GameObject.DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        ShipPosition();   
    }

    // Function for (mainly) handling ship positioning, but it also handles when the ship should shoot
    void ShipPosition() {

        // If a touch is detected
        if (Input.touchCount > 0)
        {
            // Get the touch input, and sets touch position = to where the finger is touching the screen
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began) //If the screen is just touched
            { 
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition); // Allows overlap with certain collision to prevent erros
            }
            if (touch.phase == TouchPhase.Stationary) // If the finger is staionary on the screen
            {
                bulletBehavior(fltBulletFireRate);    // Fires bullets
            }
            if (touch.phase == TouchPhase.Moved)      // If the finger is moving
            {
                transform.position = new Vector2(touchPosition.x, touchPosition.y);  // Sets position of object to the finger location
                bulletBehavior(fltBulletFireRate);                                   // Fires bullets
            }
            if (touch.phase == TouchPhase.Ended)     // If the finger lets go of the screen
            {
                
            }

        }

    }

    // Function for bullet shooting behavior
    // Instantiates a bullet object and fires it, but there is a timer to prevent a new instance being created on each frame
    void bulletBehavior(float fltFireRate)
    {
        if (Time.time >= fltTimer)
        {
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
            fltTimer = Time.time + fltFireRate;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipBehavior : MonoBehaviour {

    // Collision and Game Object data for player
    public static ShipBehavior Instance;
    private Rigidbody2D rb;
    private Collider2D col;
    public GameObject left;
    public GameObject right;
    public Transform player;

    // Collision and Game Object data for bullets
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public float fltBulletFireRate;

    // Used in ShipPosition() function
    private Vector3 touchPosition;
    private Vector3 direction;
    public float fltMoveSpeed;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    // Timers for the needed functions
    // fltTimer is for timing bullets, fltInvincibilityTimer is for timing player invincibility after being hit
    private float fltTimer = 0;
    private float fltInvincibilityTimer = 0;

    private bool boolLeft = true;
    private bool boolPlayerTouched = false;

    private int intLife = 3;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

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

    void ShipPosition()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);     // Get the position of touch in relation to the camera
            touchPosition.z = 0;
            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                startTouchPosition = Input.GetTouch(0).position;
                if (col == touchedCollider)
                {
                    boolPlayerTouched = true;
                }
            }
            if (touch.phase == TouchPhase.Moved)
            {
                if(boolPlayerTouched)
                {
                    direction = (touchPosition - transform.position);                   // Sets the velocity direction using the touchposition, and what position the item is supposed to transform to
                    rb.velocity = new Vector2(direction.x, direction.y) * fltMoveSpeed; // Velocity of the object is set to its x direction, and y direction * the speed variable
                    bulletBehavior(fltBulletFireRate);                                  // Updates bullet fire
                }
            }
            if (touch.phase == TouchPhase.Stationary)
            {
                if(boolPlayerTouched)
                {
                    bulletBehavior(fltBulletFireRate);
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = Input.GetTouch(0).position;
                if (endTouchPosition.x < startTouchPosition.x && boolPlayerTouched == false && boolLeft == true)
                {
                    /*SceneManager.LoadScene("SwappedLevel");
                    boolLeft = false;*/

                    left.SetActive(false);
                    right.SetActive(true);
                    boolLeft = false;

                }
                if (endTouchPosition.x > startTouchPosition.x && boolPlayerTouched == false && boolLeft == false)
                {
                    /*SceneManager.LoadScene("TestLevel");
                    boolLeft = true;*/

                    left.SetActive(true);
                    right.SetActive(false);
                    boolLeft = true;

                }
                rb.velocity = Vector2.zero;
                boolPlayerTouched = false;
            }
        }
    }

    // Function for bullet shooting behavior
    // Instantiates a bullet object and fires it, but there is a timer to prevent a new instance being created on each frame
    void bulletBehavior(float fltFireRate)
    {
        if (Time.time >= fltTimer)
        {
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation, player);
            fltTimer = Time.time + fltFireRate;
        }
    }

    // When the ship enters a "trigger" box
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Prevents anything from happening when it detects a bullet from entering (temp)
        if (other.gameObject.tag == "Bullet")
        {

        }
        else if (other.gameObject.tag == "EnemyBullet")
        {
            takeDamage(other);
        }

    }

    private void takeDamage(Collider2D other)
    {
        if (Time.time >= fltInvincibilityTimer)
        {
            Destroy(other.gameObject);
            intLife--;
            if (intLife <= 0)
            {
                Destroy(this.gameObject);
            }
            fltInvincibilityTimer = Time.time + 3f;
        }
        
    }

}

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
    private Rigidbody2D rb;

    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public float fltBulletFireRate;

    private Vector3 touchPosition;
    private Vector3 direction;

    public float fltMoveSpeed;
    private float fltTimer = 0;
    private float fltInvincibilityTimer = 0;

    private bool boolLeft = true;

    private int intLife = 3;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

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
        if (Input.touchCount > 0)    // If the screen is being touched
        {
            Touch touch = Input.GetTouch(0);                                     // Get the information of where the screen is touched, and set it to touch
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);     // Get the position of touch in relation to the camera
            touchPosition.z = 0;                                                // Sets rortation to 0
            direction = (touchPosition - transform.position);                   // Sets the velocity direction using the touchposition, and what position the item is supposed to transform to
            rb.velocity = new Vector2(direction.x, direction.y) * fltMoveSpeed; // Velocity of the object is set to its x direction, and y direction * the speed variable
            bulletBehavior(fltBulletFireRate);                                  // Updates bullet fire

            if (touch.phase == TouchPhase.Ended)                                // If finger is taken off screen
            {
                rb.velocity = Vector2.zero;                                     // Set velocity of the object to 0
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
        else if (boolLeft == true && other.gameObject.tag == "SwapRight" && rb.velocity.x > 20)     // If you are on the left screen, and the collision box's tag is SwapRight, change to other scene
        {
            SceneManager.LoadScene("SwappedLevel");
            boolLeft = false;
        }
        else if (boolLeft == false && other.gameObject.tag == "SwapLeft" && rb.velocity.x < -20)     // If you are on the right screen, and the collision box's tag is SwapLeft, change to other scene
        {
            SceneManager.LoadScene("TestLevel");
            boolLeft = true;
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

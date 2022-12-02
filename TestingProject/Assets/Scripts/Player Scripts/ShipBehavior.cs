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
    public GameObject UpgradeScreen;
    public GameObject right;
    public GameObject oneCannon;
    public GameObject twoCannon;
    public GameObject pauseMenuUI;
    public Transform player;
    public GameObject gameOverUI;
    private GameObject[] bullets;
   
    // Collision and Game Object data for bullets
    public Transform shootingPoint;
    public Transform shootingPoint1;
    public Transform shootingPoint2;
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public float fltBulletFireRate;
    public bool upgradedGuns = false;

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
    private bool boolUpgradeScreen = false;
    private bool boolPlayerTouched = false;

    public int intLife = 3;

    public bool boolShipUpgraded = false; // checks if ship has been upgraded on upgraded screen


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

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
                    if(oneCannon.activeInHierarchy)
                    {
                        bulletBehavior(fltBulletFireRate);                              // Updates bullet fire
                    } else
                    {
                        bulletBehavior2(fltBulletFireRate);                             // Updates bullet fire
                    }
                                                      
                }
            }
            if (touch.phase == TouchPhase.Stationary)
            {
                if(boolPlayerTouched)
                {
                    if (oneCannon.activeInHierarchy)
                    {
                        bulletBehavior(fltBulletFireRate);                              // Updates bullet fire
                    }
                    else
                    {
                        bulletBehavior2(fltBulletFireRate);                             // Updates bullet fire
                    }
                }
            }
            if (touch.phase == TouchPhase.Ended || boolShipUpgraded == true)
            {
                if (!pauseMenuUI.activeInHierarchy)
                {
                    endTouchPosition = Input.GetTouch(0).position;
                    if (endTouchPosition.x < startTouchPosition.x && boolPlayerTouched == false && boolLeft == true && boolUpgradeScreen == false && Mathf.Abs(endTouchPosition.x - startTouchPosition.x) > 100 && SwapBar.instance.slider.value == 100) // if the user swipes to the right with a full swap bar
                    {
                        SwapBar.instance.slider.value = 0; // set swap bar to 0
                        DestoryAllBullet(); // destroy all bullet objects
                        left.SetActive(false); // set left screen to inactive
                        UpgradeScreen.SetActive(true); // set upgrade screen to active
                        boolUpgradeScreen = true; // set boolean for upgrade screen to true
                    }

                    if (boolShipUpgraded == true) // something changes on upgrade screen
                    {
                        SwapBar.instance.slider.value = 0; // set swap bar to 0
                        DestoryAllBullet(); // destroy all bullet objects
                        if(boolLeft == true) // if left screen was last screen
                        {
                            right.SetActive(true);  // set right as active
                            boolLeft = false;
                        } 
                        else // if right screen was last screen
                        {
                            left.SetActive(true);  // set left as active
                            boolLeft = true;
                        }
                        UpgradeScreen.SetActive(false); // set upgradescreen to inactive
                        boolUpgradeScreen = false; 
                        boolShipUpgraded = false;
                    }

                    if (endTouchPosition.x > startTouchPosition.x && boolPlayerTouched == false && boolLeft == false && boolUpgradeScreen == false && Mathf.Abs(endTouchPosition.x - startTouchPosition.x) > 100 && SwapBar.instance.slider.value == 100) // if the user swipes to the left with a full swap bar
                    {

                        SwapBar.instance.slider.value = 0; // set swap bar to 0
                        DestoryAllBullet(); // destroy all bullet objects
                        right.SetActive(false); // set left screen to inactive
                        UpgradeScreen.SetActive(true); // set upgrade screen to active 
                        boolUpgradeScreen = true; // set boolean for upgrade screen to true
                    }
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
        if (Time.time >= fltTimer && boolUpgradeScreen == false)
        {
            if(upgradedGuns == false)
            {
                Instantiate(bulletPrefab, shootingPoint.position, transform.rotation, player);
                fltTimer = Time.time + fltFireRate;
            } else
            {
                Instantiate(bulletPrefab2, shootingPoint.position, transform.rotation, player);
                fltTimer = Time.time + fltFireRate;
            }
            
        }
    }


    void bulletBehavior2(float fltFireRate)
    {
        if (Time.time >= fltTimer && boolUpgradeScreen == false)
        {
            if(upgradedGuns == false)
            {
                Instantiate(bulletPrefab, shootingPoint1.position, transform.rotation, player);
                Instantiate(bulletPrefab, shootingPoint2.position, transform.rotation, player);
                fltTimer = Time.time + fltFireRate;
            } else
            {
                Instantiate(bulletPrefab2, shootingPoint1.position, transform.rotation, player);
                Instantiate(bulletPrefab2, shootingPoint2.position, transform.rotation, player);
                fltTimer = Time.time + fltFireRate;
            }
            
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
            takeDamage(other, 1);
        }
        else if (other.gameObject.tag == "SuicideBomber")
        {
            takeDamage(other, 5);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            takeDamage(other, 5);
        }

    }

    private void takeDamage(Collider2D other, int intDamageDone)
    {
        if (Time.time >= fltInvincibilityTimer)
        {
            Destroy(other.gameObject);
            intLife = intLife - intDamageDone;
            if (intLife <= 0)
            {
                gameOver();

            }
            fltInvincibilityTimer = Time.time + 3f;
        }
        
    }

    private void gameOver()
    {
        gameOverUI.SetActive(true);
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        Time.timeScale = 0f;
    }

    public void restartButton()
    {
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("TestLevel");
    }

    public void quitButton()
    {
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void DestroyBullets(string tag)
    {
        bullets = GameObject.FindGameObjectsWithTag(tag);
        for(int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i]);
        }
    }

    public void DestoryAllBullet()
    {
        DestroyBullets("Bullet");
        DestroyBullets("EnemyBullet");
        DestroyBullets("UpgradedBullet");
    }




}

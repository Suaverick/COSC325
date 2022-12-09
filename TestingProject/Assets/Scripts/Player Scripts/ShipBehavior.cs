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

    public HealthBar healthBar;

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

    // Booleans
    private bool boolLeft = true;
    private bool boolUpgradeScreen = false;
    private bool boolPlayerTouched = false;
    public bool boolShipUpgraded = false; // checks if ship has been upgraded on upgraded screen

    public int maxHealth = 20;
    public int currentHealth;

    // Audo clips are stored here
    public AudioClip laserShot;
    public AudioClip damageHit;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // FixedUpdate is called every frame, regardless of lag
    // Don't use Update because we are doing physics calculations for moving the ship
    void FixedUpdate()
    {
        ShipPosition();
        healthBar.SetHealth(currentHealth);
    }

    // Handles ship positioning 
    void ShipPosition()
    {
        if (Input.touchCount > 0)  // If the player is touching the screen
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);     // Get the position of touch in relation to the camera
            touchPosition.z = 0;
            if (touch.phase == TouchPhase.Began) // If the user just started touching the screen
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                startTouchPosition = Input.GetTouch(0).position;
                if (col == touchedCollider) // If the user touched the ship
                {
                    boolPlayerTouched = true;
                }
            }
            if (touch.phase == TouchPhase.Moved) // If the player's finger is moving
            {
                if(boolPlayerTouched) // If the player is still touching the ship
                {
                    direction = (touchPosition - transform.position);                   // Sets the velocity direction using the touchposition, and what position the item is supposed to transform to
                    rb.velocity = new Vector2(direction.x, direction.y) * fltMoveSpeed; // Velocity of the object is set to its x direction, and y direction * the speed variable
                    if(oneCannon.activeInHierarchy)                                     // One shooting point is active
                    {
                        bulletBehavior(fltBulletFireRate);                              // Updates bullet fire
                    } else                                                              // Two shooting points are active
                    {
                        bulletBehavior2(fltBulletFireRate);                             // Updates bullet fire
                    }
                                                      
                }
            }
            if (touch.phase == TouchPhase.Stationary)                                   // If the player has their finger down, but not moving
            {
                if(boolPlayerTouched)
                {
                    if (oneCannon.activeInHierarchy)                                    // If one shooting point is active
                    {
                        bulletBehavior(fltBulletFireRate);                              // Updates bullet fire
                    }
                    else                                                                // If two shooting points are active
                    {
                        bulletBehavior2(fltBulletFireRate);                             // Updates bullet fire
                    }
                    //bulletBehavior(fltBulletFireRate);
                    rb.velocity = Vector2.zero;                                         // Velocity is set to 0 when stationary to prevent bugs
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
                        healthBar.SetHealth(maxHealth);
                        SwapBar.instance.slider.value = 0; // set swap bar to 0
                        DestoryAllBullet(); // destroy all bullet objects
                        if(boolLeft == true) // if left screen was last screen
                        {
                            // force player to fly forward out of screen bounds

                            right.SetActive(true);  // set right as active
                            boolLeft = false;
                        } 
                        else // if right screen was last screen
                        {
                            // force player to fly forward out of screen bounds

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
            if(upgradedGuns == false)       // Non-upgraded bullets
            {
                Instantiate(bulletPrefab, shootingPoint.position, transform.rotation, player);
                audioSource.PlayOneShot(laserShot);
                fltTimer = Time.time + fltFireRate;
            } else                          // Upgraded bullets
            {
                Instantiate(bulletPrefab2, shootingPoint.position, transform.rotation, player);
                audioSource.PlayOneShot(laserShot);
                fltTimer = Time.time + fltFireRate;
            }
            
        }
    }

    // Function for bullet shooting behavior when two shooting points are active
    void bulletBehavior2(float fltFireRate)
    {
        if (Time.time >= fltTimer && boolUpgradeScreen == false)
        {
            if(upgradedGuns == false)  // If upgrade is not enabled
            {
                Instantiate(bulletPrefab, shootingPoint1.position, transform.rotation, player);
                Instantiate(bulletPrefab, shootingPoint2.position, transform.rotation, player);
                audioSource.PlayOneShot(laserShot);
                fltTimer = Time.time + fltFireRate;
            } else                    // If upgrade is enabled
            {
                Instantiate(bulletPrefab2, shootingPoint1.position, transform.rotation, player);
                Instantiate(bulletPrefab2, shootingPoint2.position, transform.rotation, player);
                audioSource.PlayOneShot(laserShot);
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
            Destroy(other.gameObject);
            takeDamage(1);
        }
        else if (other.gameObject.tag == "SuicideBomber")
        {
            Destroy(other.gameObject);
            takeDamage(30);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            takeDamage(20);
        }
        else if (other.gameObject.tag == "Missle")
        {
            Destroy(other.gameObject);
            takeDamage(2);
        }
        else if (other.gameObject.tag == "EnemyBeam")
        {
            takeDamage(3);
        }
        else if (other.gameObject.tag == "Boss")
        {
            takeDamage(20);
        }
        else if (other.gameObject.tag == "HellBoss")
        {
            takeDamage(5);
        }
        else if (other.gameObject.tag == "SpaceBoss")
        {
            takeDamage(20);
        }
        else if (other.gameObject.tag == "SpaceBossWave")
        {
            takeDamage(5);
        }

    }

    private void takeDamage(int intDamageDone)
    {
        if (Time.time >= fltInvincibilityTimer)
        {
            currentHealth = currentHealth - intDamageDone;
            audioSource.PlayOneShot(damageHit);
            healthBar.SetHealth(currentHealth);
            if (currentHealth <= 0)
            {
                gameOver();
            }
            fltInvincibilityTimer = Time.time + 1f;
        }
    }

    // When the player runs out of health
    public void gameOver()
    {
        gameOverUI.SetActive(true);
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        Time.timeScale = 0f;
    }

    // When the player hits the restart button in the game over menu
    public void restartButton()
    {
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // When the player hits the quit button in the game over menu
    public void quitButton()
    {
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // Gets an array of bullets with a specified tag, and destorys each one
    public void DestroyBullets(string tag)
    {
        bullets = GameObject.FindGameObjectsWithTag(tag);
        if (bullets.Length != 0)
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                Destroy(bullets[i]);
            }
        }
    }

    // Used to destory all bullets between screen swaps
    public void DestoryAllBullet()
    {
        DestroyBullets("Bullet");
        DestroyBullets("EnemyBullet");
        DestroyBullets("UpgradedBullet");
        DestroyBullets("EnemyBeam");
        DestroyBullets("Missle");
        DestroyBullets("SpaceBossWave");
    }

}

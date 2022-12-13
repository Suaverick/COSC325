using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellBossBehavior : MonoBehaviour
{

    // Game Object variables for HellBoss
    public GameObject bossMatterBasic;
    public GameObject bossMatterDecoy;
    public GameObject bossMatterSpinner;
    public GameObject bossMatterSpinnerDecoy;
    public GameObject bossMatterLocked;
    private GameObject spinner;
    private GameObject decoySpinner;

    private GameObject[] matter;

    // Vectors for HellBoss
    private Vector2 spawnPosition;
    private Vector2 targetPosition;

    // Health of the boss
    private int intHealth = 200;

    // How much the boss rotates
    private float fltRotationAmount;

    // All boolean switches needed for the boss
    private bool boolTeleport = false;

    public bool boolPhase1 = false;
    public bool bool1to2 = false;
    public bool boolPhase2 = false;
    public bool bool2to3 = false;
    public bool boolPhase3 = false;

    private bool boolMatterOn = false;
    private bool boolLockedMatterOn = false;
    private bool boolFinalPhaseOn = false;
    private bool boolDecoysSpawned = false;
    private bool boolDecoysSpawned2 = false;
    private bool boolDecoySpinnersSpawned = false;

    private bool boolAtPosition = false;
    private bool boolSwitch = false;

    public bool boolDead = false;

    // Movement speed for the boss
    private float fltMoveSpeed = 2f;

    // Needed starting position for the hell boss
    private Vector3 startPosition;

    public AudioClip hitShot;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //boolPhase1 = true;
        spawnPosition = new Vector2(transform.position.x, transform.position.y - 8f);
        targetPosition = new Vector2(transform.position.x, -15);
        audioSource = GetComponent<AudioSource>();
        startPosition = gameObject.transform.position;
        startPosition.y = startPosition.y - 8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position != startPosition && !boolAtPosition)        // If hell boss is not at the correct position, move there
        {
            Vector3 newPos = Vector3.MoveTowards(gameObject.transform.position, startPosition, fltMoveSpeed * Time.deltaTime);
            gameObject.transform.position = newPos;
        }
        if (gameObject.transform.position == startPosition)                          // If at the correct position, stop moving and set phase1 to active
        {
            if (!boolSwitch)
            {
                boolSwitch = true;
                boolAtPosition = true;
                boolPhase1 = true;
            }
        }
        if (boolPhase1) phase1();
        if (bool1to2) phase1to2();
        if (boolPhase2) phase2();
        if (bool2to3) phase2to3();
        if (boolPhase3) phase3();
    }

    // Spawns the basic form of matter that the boss shoots
    void spawnMatter()
    {
        Instantiate(bossMatterBasic, new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y - 2f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterBasic, new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterBasic, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 3f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterBasic, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterBasic, new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y - 2f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
    }

    // Spawns decoy matter that looks the exact same, but does not shoot any poison
    void spawnDecoys()
    {
        Instantiate(bossMatterDecoy, new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y - 2f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterDecoy, new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterDecoy, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 3f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterDecoy, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterDecoy, new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y - 2f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        DestroyMatter("Matter");
    }

    // Spawns the matter spinners that shoot at the player
    void spawnSpinners()
    {
        // Bottom Half of Spinners
        Instantiate(bossMatterSpinner, new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y - 2f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 45), gameObject.transform);
        Instantiate(bossMatterSpinner, new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 22.5f), gameObject.transform);
        Instantiate(bossMatterSpinner, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 3f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0,0,0), gameObject.transform);
        Instantiate(bossMatterSpinner, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, -22.5f), gameObject.transform);
        Instantiate(bossMatterSpinner, new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y - 2f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, -45), gameObject.transform);

        // Top Half of Spinners
        Instantiate(bossMatterSpinner, new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y + 2f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 135), gameObject.transform);
        Instantiate(bossMatterSpinner, new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y + 2.5f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 155.5f), gameObject.transform);
        Instantiate(bossMatterSpinner, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 180), gameObject.transform);
        Instantiate(bossMatterSpinner, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y + 2.5f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, -155.5f), gameObject.transform);
        Instantiate(bossMatterSpinner, new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y + 2f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, -135), gameObject.transform);
        
        // Left Half of Spinners
        Instantiate(bossMatterSpinner, new Vector3(gameObject.transform.position.x + 2.5f, gameObject.transform.position.y + 1f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, -247.5f), gameObject.transform);
        Instantiate(bossMatterSpinner, new Vector3(gameObject.transform.position.x + 3f, gameObject.transform.position.y, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, -270), gameObject.transform);
        Instantiate(bossMatterSpinner, new Vector3(gameObject.transform.position.x + 2.5f, gameObject.transform.position.y - 1f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, -292.5f), gameObject.transform);

        // Right Half of Spinners
        Instantiate(bossMatterSpinner, new Vector3(gameObject.transform.position.x - 2.5f, gameObject.transform.position.y + 1f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 247.5f), gameObject.transform);
        Instantiate(bossMatterSpinner, new Vector3(gameObject.transform.position.x - 3f, gameObject.transform.position.y, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 270), gameObject.transform);
        spinner = Instantiate(bossMatterSpinner, new Vector3(gameObject.transform.position.x - 2.5f, gameObject.transform.position.y - 1f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 292.5f), gameObject.transform);

    }

    // Spawns a decoy of the spinners that looks the exact same, but doesn't shoot
    void spawnDecoySpinners()
    {
        // Bottom Half of Spinners
        Instantiate(bossMatterSpinnerDecoy, new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y - 2f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 45), gameObject.transform);
        Instantiate(bossMatterSpinnerDecoy, new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 22.5f), gameObject.transform);
        Instantiate(bossMatterSpinnerDecoy, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 3f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 0), gameObject.transform);
        Instantiate(bossMatterSpinnerDecoy, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, -22.5f), gameObject.transform);
        Instantiate(bossMatterSpinnerDecoy, new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y - 2f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, -45), gameObject.transform);

        // Top Half of Spinners
        Instantiate(bossMatterSpinnerDecoy, new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y + 2f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 135), gameObject.transform);
        Instantiate(bossMatterSpinnerDecoy, new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y + 2.5f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 155.5f), gameObject.transform);
        Instantiate(bossMatterSpinnerDecoy, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 180), gameObject.transform);
        Instantiate(bossMatterSpinnerDecoy, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y + 2.5f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, -155.5f), gameObject.transform);
        Instantiate(bossMatterSpinnerDecoy, new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y + 2f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, -135), gameObject.transform);

        // Left Half of Spinners
        Instantiate(bossMatterSpinnerDecoy, new Vector3(gameObject.transform.position.x + 2.5f, gameObject.transform.position.y + 1f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, -247.5f), gameObject.transform);
        Instantiate(bossMatterSpinnerDecoy, new Vector3(gameObject.transform.position.x + 3f, gameObject.transform.position.y, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, -270), gameObject.transform);
        Instantiate(bossMatterSpinnerDecoy, new Vector3(gameObject.transform.position.x + 2.5f, gameObject.transform.position.y - 1f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, -292.5f), gameObject.transform);

        // Right Half of Spinners
        Instantiate(bossMatterSpinnerDecoy, new Vector3(gameObject.transform.position.x - 2.5f, gameObject.transform.position.y + 1f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 247.5f), gameObject.transform);
        Instantiate(bossMatterSpinnerDecoy, new Vector3(gameObject.transform.position.x - 3f, gameObject.transform.position.y, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 270), gameObject.transform);
        decoySpinner = Instantiate(bossMatterSpinnerDecoy, new Vector3(gameObject.transform.position.x - 2.5f, gameObject.transform.position.y - 1f, gameObject.transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 292.5f), gameObject.transform);

    }

    // Spawn a different type of matter where the poison locks to the player
    void spawnMatterLocked()
    {
        Instantiate(bossMatterLocked, new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y - 2f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterLocked, new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterLocked, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 3f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterLocked, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterLocked, new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y - 2f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
    }

    // Phase 1 for the boss
    void phase1()
    {
        if (!boolMatterOn) {
            boolMatterOn = true;
            fltRotationAmount = 0;
            spawnMatter();
        }
    }

    // Phase 1 to 2 transition
    public void phase1to2()
    {
        if (!boolDecoysSpawned)                     // If the decoys were spawned
        {
            boolDecoysSpawned = true;
            spawnDecoys();
            DestroyMatter("Matter");
            spawnSpinners();
            DestroyMatter("DecoyMatter");
        }

        if (fltRotationAmount < 1080 && spinner.transform.localScale.y >= 0.8f)                 // If the spinner has a scale of 0.8, begin rotating
        {
            gameObject.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 3f);
            fltRotationAmount += 3f;
        }
        else if (fltRotationAmount < 1080 && spinner.transform.localScale.y <= 0.8f)           // If the spinner is 0.8 scale, but the boss hasn't finished rotating
        {
            // Do nothing
        }
        else
        {
            if(!boolDecoySpinnersSpawned)                               // If decoy spinners spawned
            {
                boolDecoySpinnersSpawned = true;
                spawnDecoySpinners();
                spawnDecoys();
                DestroyMatter("BossSpinner");
            }

            if (decoySpinner.transform.localScale.y <= 0.02f)          // If decoy spinner is less than a scale of 0.02f, go to phase 2
            {
                DestroyMatter("SpinnerDecoy");
                DestroyMatter("DecoyMatter");
                bool1to2 = false;
                boolPhase2 = true;
            }
            else
            {
                // Do nothing
            }
        }
    }

    // Used by observer
    // Skips from the phase1to2 transition and goes straight to phase2
    public void phase1to2skip()
    {
        bool1to2 = false;
        DestroyMatter("BossSpinner");
        DestroyMatter("SpinnerDecoy");
        DestroyMatter("DecoyMatter");
        transform.rotation = Quaternion.Euler(0, 0, 0);
        boolPhase2 = true;
    }

    // Phase 2 for the boss
    void phase2()
    {

        if(!boolLockedMatterOn)
        {
            DestroyMatter("BossSpinner");
            DestroyMatter("SpinnerDecoy");
            DestroyMatter("DecoyMatter");
            boolLockedMatterOn = true;
            fltRotationAmount = 0;
            spawnMatterLocked();
        }
    }

    // Phase 2 to 3 transition
    public void phase2to3()
    {
        if(!boolDecoysSpawned2)
        {
            boolDecoysSpawned2 = true;
            spawnDecoys();
            DestroyMatter("Matter");
            spawnSpinners();
        }
        if (spinner.transform.localScale.y >= 0.3f) {     // If the spinner's scale is greater than 0.3f
            if (fltRotationAmount < 360)
            {
                gameObject.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 3f);
                fltRotationAmount += 3f;
            }
            else if (fltRotationAmount >= 360 && fltRotationAmount < 1440)
            {
                gameObject.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 1.5f);
                fltRotationAmount += 1.5f;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, 4 * Time.deltaTime);
            }
            else if (fltRotationAmount < 2160)
            {
                if (!boolTeleport)
                {
                    boolTeleport = true;
                    transform.position = new Vector2(0, 15);
                }
                gameObject.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 1.5f);
                fltRotationAmount += 1.5f;
                transform.position = Vector2.MoveTowards(transform.position, spawnPosition, 4 * Time.deltaTime);
            }
            else
            {
                DestroyMatter("DecoyMatter");
                bool2to3 = false;
                boolPhase3 = true;
            }
        }
        else
        {
            // Do nothing
        }
    }
    // Used by observer
    // Skips to phase 3
    public void phase2to3skip()
    {
        bool2to3 = false;
        DestroyMatter("DecoyMatter");
        gameObject.transform.position = spawnPosition;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        boolPhase3 = true;

    }

    // Phase 3 of the boss
    void phase3()
    {
        if(!boolFinalPhaseOn)
        {
            boolFinalPhaseOn = true;
            fltRotationAmount = 0;
            spawnMatter();
        }
    }

    // Destroy Matter with the specified tag
    public void DestroyMatter(string tag)
    {
        matter = GameObject.FindGameObjectsWithTag(tag);
        if (matter.Length != 0)
        {
            for (int i = 0; i < matter.Length; i++)
            {
                Destroy(matter[i]);
            }
        }
    }

    // When a bullet enters the collision box of the hell boss
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (!bool1to2 && !bool2to3)
            {
                takeDamage(other, 1);
            }
        }
        if(other.CompareTag("UpgradedBullet"))
        {
            if (!bool1to2 && !bool2to3)
            {
                takeDamage(other, 2);
            }
        }
    }

    // Function that handels damage and phases
    public void takeDamage(Collider2D other, int intDamageTaken)
    {
        Destroy(other.gameObject);
        audioSource.PlayOneShot(hitShot);
        intHealth = intHealth - intDamageTaken;
        if (intHealth <= 133 && boolPhase1)
        {
            boolPhase1 = false;
            bool1to2 = true;
        }
        if (intHealth <= 67 && boolPhase2)
        {
            boolPhase2 = false;
            bool2to3 = true;
        }
        if (intHealth <= 0)
        {
            ScoreManager.instance.AddPoint();
            SwapBar.instance.IncrementProgress(100f);
            boolDead = true;
            Destroy(gameObject);
        }
    }

}

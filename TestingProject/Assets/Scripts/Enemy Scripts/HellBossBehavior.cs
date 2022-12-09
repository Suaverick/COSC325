using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellBossBehavior : MonoBehaviour
{

    public GameObject bossMatterBasic;
    public GameObject bossMatterDecoy;
    public GameObject bossMatterSpinner;
    public GameObject bossMatterSpinnerDecoy;
    public GameObject bossMatterLocked;

    private Vector2 spawnPosition;
    private Vector2 targetPosition;

    private int intHealth = 200;

    private float fltRotationAmount;

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

    private GameObject spinner;
    private GameObject decoySpinner;

    private GameObject[] matter;

    private bool boolAtPosition = false;
    private bool boolSwitch = false;

    private float fltMoveSpeed = 2f;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        //boolPhase1 = true;
        spawnPosition = new Vector2(transform.position.x, transform.position.y - 8f);
        targetPosition = new Vector2(transform.position.x, -15);

        startPosition = gameObject.transform.position;
        startPosition.y = startPosition.y - 8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position != startPosition && !boolAtPosition)
        {
            Vector3 newPos = Vector3.MoveTowards(gameObject.transform.position, startPosition, fltMoveSpeed * Time.deltaTime);
            gameObject.transform.position = newPos;
        }
        if (gameObject.transform.position == startPosition)
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

    void spawnMatter()
    {
        Instantiate(bossMatterBasic, new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y - 2f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterBasic, new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterBasic, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 3f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterBasic, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterBasic, new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y - 2f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
    }

    void spawnDecoys()
    {
        Instantiate(bossMatterDecoy, new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y - 2f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterDecoy, new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterDecoy, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 3f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterDecoy, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterDecoy, new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y - 2f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        DestroyMatter("Matter");
    }

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

    void spawnMatterLocked()
    {
        Instantiate(bossMatterLocked, new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y - 2f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterLocked, new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterLocked, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 3f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterLocked, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
        Instantiate(bossMatterLocked, new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y - 2f, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0), gameObject.transform);
    }

    void phase1()
    {
        if (!boolMatterOn) {
            boolMatterOn = true;
            fltRotationAmount = 0;
            spawnMatter();
        }
    }

    public void phase1to2()
    {
        if (!boolDecoysSpawned)
        {
            boolDecoysSpawned = true;
            spawnDecoys();
            DestroyMatter("Matter");
            spawnSpinners();
            DestroyMatter("DecoyMatter");
        }

        if (fltRotationAmount < 1080 && spinner.transform.localScale.y >= 0.8f)
        {
            gameObject.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 3f);
            fltRotationAmount += 3f;
        }
        else if (fltRotationAmount < 1080 && spinner.transform.localScale.y <= 0.8f)
        {
            // Do nothing
        }
        else
        {
            if(!boolDecoySpinnersSpawned)
            {
                boolDecoySpinnersSpawned = true;
                spawnDecoySpinners();
                spawnDecoys();
                DestroyMatter("BossSpinner");
            }

            if (decoySpinner.transform.localScale.y <= 0.02f)
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

    public void phase1to2skip()
    {
        bool1to2 = false;
        DestroyMatter("BossSpinner");
        DestroyMatter("SpinnerDecoy");
        DestroyMatter("DecoyMatter");
        transform.rotation = Quaternion.Euler(0, 0, 0);
        boolPhase2 = true;
    }

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

    public void phase2to3()
    {
        if(!boolDecoysSpawned2)
        {
            boolDecoysSpawned2 = true;
            spawnDecoys();
            DestroyMatter("Matter");
            spawnSpinners();
        }
        if (spinner.transform.localScale.y >= 0.3f) {
            if (fltRotationAmount < 360)
            {
                gameObject.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 1.5f);
                fltRotationAmount += 1.5f;
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

    public void phase2to3skip()
    {
        bool2to3 = false;
        DestroyMatter("DecoyMatter");
        gameObject.transform.position = spawnPosition;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        boolPhase3 = true;

    }

    void phase3()
    {
        if(!boolFinalPhaseOn)
        {
            boolFinalPhaseOn = true;
            fltRotationAmount = 0;
            spawnMatter();
        }
    }

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

    public void takeDamage(Collider2D other, int intDamageTaken)
    {
        Destroy(other.gameObject);
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
            Destroy(gameObject);
        }
    }

}

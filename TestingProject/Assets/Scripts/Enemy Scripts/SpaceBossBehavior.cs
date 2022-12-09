using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBossBehavior : MonoBehaviour
{

    public GameObject misslePrefab;
    public GameObject bossBeam;
    public GameObject bossWave;
    private GameObject[] beams;
    public GameObject guard;

    private Renderer rend;

    public Transform shootingPoint1;
    public Transform shootingPoint2;
    public Transform shootingPoint3;
    public Transform shootingPoint4;

    public Transform beamPoint1;
    public Transform beamPoint2;
    public Transform beamPoint3;
    public Transform beamPoint4;
    public Transform beamPoint5;

    public Transform wavePoint;

    public Transform finalPhase;

    Vector3 spawnPosition;

    public float fltBulletFireRate;
    private float fltMoveSpeed = 2f;
    private float fltTimer = 0;
    private float fltWaitTimer = 0;
    private float fltPatternTime = 1.0f;

    public int intMisslesShot;
    private int intHealth = 300;

    public bool boolPhase1 = false;
    public bool boolPhase2 = false;
    public bool boolPhase3 = false;
    public bool boolWaitOn = false;
    public bool boolPatternOn = false;
    public bool boolPatternAndMissleOn = false;
    public bool bool1to2 = false;
    public bool bool2to3 = false;
    public bool boolTransitionSwitch = false;
    public bool boolGuardsSpawned = false;
    public bool boolOneWave = false;

    private bool boolAtPosition = false;
    private bool boolSwitch = false;

    [SerializeField]
    private Color colorToTurnTo = Color.black;
    [SerializeField]
    private Color colorToTurnBackTo = Color.white;

    private Vector3 startPosition;

    public AudioClip beamSound;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        //boolPhase1 = true;

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
        if (gameObject.transform.position == spawnPosition)
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

    void phase1()
    {
        shootMissles(fltBulletFireRate);
    }

    void shootMissles(float fltFireRate)
    {
        if (Time.time >= fltTimer && intMisslesShot <= 3)
        {
            Instantiate(misslePrefab, shootingPoint1.position, transform.rotation, shootingPoint1);
            Instantiate(misslePrefab, shootingPoint2.position, transform.rotation, shootingPoint2);
            Instantiate(misslePrefab, shootingPoint3.position, transform.rotation, shootingPoint3);
            Instantiate(misslePrefab, shootingPoint4.position, transform.rotation, shootingPoint4);
            intMisslesShot++;
            fltTimer = Time.time + fltFireRate;
        }
        else if (Time.time <= fltTimer && intMisslesShot <= 3)
        {

        }
        else
        {
            waitBetweenShots();
        }
    }

    void waitBetweenShots()
    {
        if (!boolWaitOn)
        {
            fltWaitTimer = Time.time + 3f;
            boolWaitOn = true;
        }
        if (Time.time >= fltWaitTimer)
        {
            intMisslesShot = 0;
            boolWaitOn = false;
        }
    }

    void phase2()
    {
        if (!boolPatternOn)
        {
            StartCoroutine(phase2pattern());
        }
    }

    private IEnumerator phase2pattern()
    {
        boolPatternOn = true;
        // First Beam Shot
        beamBehaviorCall(true, true, false, true, true);
        yield return new WaitForSeconds(fltPatternTime*2);
        // Second Beam Shot
        beamBehaviorCall(true, false, true, false, true);
        yield return new WaitForSeconds(fltPatternTime*2);
        // Third Beam Shot
        beamBehaviorCall(false, true, true, true, false);
        yield return new WaitForSeconds(fltPatternTime * 2);
        // Fourth Beam Shot
        beamBehaviorCall(true, false, false, false, true);
        yield return new WaitForSeconds(fltPatternTime * 2);
        // Fifth Beam Shot
        beamBehaviorCall(false, false, true, true, true);
        yield return new WaitForSeconds(fltPatternTime * 2);
        boolPatternOn = false;
    }

    void beamBehaviorCall(bool beam1, bool beam2, bool beam3, bool beam4, bool beam5)
    {
        StartCoroutine(beamBehavior(beam1, beam2, beam3, beam4, beam5));
    }

    private IEnumerator beamBehavior(bool beam1, bool beam2, bool beam3, bool beam4, bool beam5)
    {
        yield return new WaitForSeconds(fltPatternTime);
        audioSource.PlayOneShot(beamSound);
        if (beam1) Instantiate(bossBeam, new Vector3(beamPoint1.position.x, beamPoint1.position.y - 4.8f, beamPoint1.position.z), transform.rotation, beamPoint1);
        if (beam2) Instantiate(bossBeam, new Vector3(beamPoint2.position.x, beamPoint2.position.y - 4.8f, beamPoint2.position.z), transform.rotation, beamPoint2);
        if (beam3) Instantiate(bossBeam, new Vector3(beamPoint3.position.x, beamPoint3.position.y - 4.8f, beamPoint3.position.z), transform.rotation, beamPoint3);
        if (beam4) Instantiate(bossBeam, new Vector3(beamPoint4.position.x, beamPoint4.position.y - 4.8f, beamPoint4.position.z), transform.rotation, beamPoint4);
        if (beam5) Instantiate(bossBeam, new Vector3(beamPoint5.position.x, beamPoint5.position.y - 4.8f, beamPoint5.position.z), transform.rotation, beamPoint5);
        yield return new WaitForSeconds(fltPatternTime);
        destroyBeams();
    }

    private IEnumerator missleBehavior(bool point1, bool point2, bool point3, bool point4)
    {
        if(!point1)Instantiate(misslePrefab, shootingPoint1.position, transform.rotation, shootingPoint1);
        if(!point2)Instantiate(misslePrefab, shootingPoint2.position, transform.rotation, shootingPoint2);
        if(!point3)Instantiate(misslePrefab, shootingPoint3.position, transform.rotation, shootingPoint3);
        if(!point4)Instantiate(misslePrefab, shootingPoint4.position, transform.rotation, shootingPoint4);
        yield return new WaitForSeconds(fltPatternTime * 2);
    }

    void destroyBeams()
    {
        beams = GameObject.FindGameObjectsWithTag("EnemyBeam");
        for (int i = 0; i < beams.Length; i++)
        {
            Destroy(beams[i]);
        }
    }

    void phase3()
    {
        if (!boolPatternAndMissleOn)
        {
            StartCoroutine(phase3Pattern());
        }
    }

    void spawnGuards()
    {
        SpawnGuard(-1.5f, 1);
        SpawnGuard(-0.5f, 1);
        SpawnGuard(0.5f, 1);
        SpawnGuard(-1.5f, 1);
        boolGuardsSpawned = true;
    }

    void SpawnGuard(float spawnPositionX, float spawnPositionY)
    {
        spawnPosition.x = spawnPositionX;
        spawnPosition.y = spawnPositionY;
        spawnPosition.z = 0;

        Vector3 offScreenSpawn;
        offScreenSpawn = spawnPosition;
        offScreenSpawn.y = spawnPosition.y + 8;

        GameObject left = GameObject.FindGameObjectWithTag("Left");
        Instantiate(guard, offScreenSpawn, guard.transform.rotation, left.transform);

    }

    private IEnumerator phase3Pattern()
    {
        boolPatternAndMissleOn = true;
        if (!bool2to3)
        {
            beamAndMissleBehaviorCall(true, false, true, false, true);
            yield return new WaitForSeconds(fltPatternTime * 2);
        }
        if (!bool2to3)
        {
            beamAndMissleBehaviorCall(true, false, true, false, true);
            yield return new WaitForSeconds(fltPatternTime * 2);
        }
        if (!bool2to3)
        {
            beamAndMissleBehaviorCall(true, true, false, true, true);
            yield return new WaitForSeconds(fltPatternTime * 2);
        }
        if (!bool2to3)
        {
            beamAndMissleBehaviorCall(true, false, false, false, true);
            yield return new WaitForSeconds(fltPatternTime * 2);
        }
        if (!bool2to3)
        {
            beamAndMissleBehaviorCall(false, false, true, false, false);
            yield return new WaitForSeconds(fltPatternTime * 2);
        }
        boolPatternAndMissleOn = false;
    }

    // thirdPoint does not exist for missle launches
    void beamAndMissleBehaviorCall(bool firstPoint, bool secondPoint, bool thirdPoint, bool fourthPoint, bool fifthPoint)
    {
        StartCoroutine(beamBehavior(firstPoint, secondPoint, thirdPoint, fourthPoint, fifthPoint));
        StartCoroutine(missleBehavior(firstPoint, secondPoint, fourthPoint, fifthPoint));
    }

    void phase1to2()
    {
        if (!boolGuardsSpawned)
        {
            spawnGuards();
        }
        if (!boolTransitionSwitch)
        {
            StopAllCoroutines();
            StartCoroutine(phaseTransition());
        }
    }

    public void phase1to2skip()
    {
        bool1to2 = false;
        rend.material.color = colorToTurnBackTo;
        boolTransitionSwitch = false;
        boolPhase2 = true;
    }

    void phase2to3()
    {
        if (!boolOneWave)
        {
            boolOneWave = true;
            Instantiate(bossWave, wavePoint.position, bossWave.transform.rotation, wavePoint);
        }
        if (!boolTransitionSwitch)
        {
            destroyBeams();
            StopAllCoroutines();
            StartCoroutine(phaseTransition2());
        }
    }

    public void phase2to3skip()
    {
        bool2to3 = false;
        rend.material.color = colorToTurnBackTo;
        boolTransitionSwitch = false;
        boolPhase3 = true;
    }

    private IEnumerator phaseTransition()
    {
        boolTransitionSwitch = true;
        rend.material.color = colorToTurnTo;
        yield return new WaitForSeconds(4.0f);
        rend.material.color = colorToTurnBackTo;
        bool1to2 = false;
        boolPhase2 = true;
        boolTransitionSwitch = false;
    }

    private IEnumerator phaseTransition2()
    {
        boolTransitionSwitch = true;
        rend.material.color = colorToTurnTo;
        yield return new WaitForSeconds(4.0f);
        rend.material.color = colorToTurnBackTo;
        bool2to3 = false;
        boolPhase3 = true;
        boolTransitionSwitch = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {
            if (!bool1to2 && !bool2to3)
            {
                takeDamage(other, 1);
            }
        }
        if (other.CompareTag("UpgradedBullet"))
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
        if (intHealth <= 200 && boolPhase1)
        {
            boolPhase1 = false;
            bool1to2 = true;
        }
        if (intHealth <= 100 && boolPhase2)
        {
            boolPhase2 = false;
            bool2to3 = true;
        }
        if (intHealth <= 0)
        {
            ScoreManager.instance.AddPoint();
            Destroy(gameObject);
        }
    }
}

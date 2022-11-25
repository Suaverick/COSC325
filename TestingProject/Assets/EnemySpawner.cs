using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{//The prefab of what enemies spawn

    public enum SpawnState {spawning, waiting, counting};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject enemy;
        public int count;
        public float rate;
    }
    public Transform spawner;
    private Vector2 location;

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float spawnCountDown = 1f;
    private SpawnState state = SpawnState.counting;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }
    void Update()

    {
        if (state == SpawnState.waiting)
        {
            //Check if enemies are dead
            if (!AreEnemiesAlive())
            {
                //UnityEngine.Debug.Log("Enemies are Dead");
                WaveCompleted();
               
            }
            else
            {
                return;
            }
        }
        if(waveCountdown <= 0)
        {
            if (state != SpawnState.spawning)
            {
                //Spawn wave
               //UnityEngine.Debug.Log(nextWave);
               // UnityEngine.Debug.Log("Enemies Incoming");
                StartCoroutine(SpawnWave(waves[nextWave]));
            }    
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }   
    }

    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.spawning;
        UnityEngine.Debug.Log("Spawning Wave: " + wave.name);
        location.Set(0, 3f);

        //Spawner

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy, location);
            location.Set(0, 3f + 1);
        }


        state = SpawnState.waiting;
        yield break;
    }

    void WaveCompleted()
    {
        //UnityEngine.Debug.Log("Wave Complted");

        state = SpawnState.counting;
        waveCountdown = timeBetweenWaves;
        if (nextWave + 1 > waves.Length - 1) 
        {
            nextWave = 0;
            //UnityEngine.Debug.Log("Wave Complted....Looping");
        }

        else
        {
            nextWave++;
        }
    }

    void SpawnEnemy(GameObject enemy, Vector2 vector)
    {

        UnityEngine.Debug.Log("Spawning Enemy: " + enemy.name);
        //Vector2 offScreenSpawn(3f, 2f);      
        Instantiate(enemy, vector, Quaternion.identity, spawner);
    }

    bool AreEnemiesAlive()  
    {
        spawnCountDown -= Time.deltaTime;
        if (spawnCountDown <= 0f)
        {
            spawnCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
          
        }
        return true;
    }
}


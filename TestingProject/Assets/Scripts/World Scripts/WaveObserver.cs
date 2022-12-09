using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WaveObserver : MonoBehaviour
{

    public GameObject leftSide;
    public GameObject rightSide;

    private bool boolLeft;
    private bool boolRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateLeft();
        updateRight();
        if(boolLeft && boolRight)
        {
            SceneManager.LoadScene("Level Select");
        }
    }

    void updateLeft()
    {
        if (leftSide.GetComponent<EnemySpawner>().waveWon && leftSide.activeInHierarchy)
        {
            boolLeft = true;
        }
    }

    void updateRight()
    {
        if(rightSide.GetComponent<EnemySpawner>().waveWon && rightSide.activeInHierarchy)
        {
            boolRight = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WaveObserver : MonoBehaviour
{

    public GameObject leftSide;
    public GameObject rightSide;
    public GameObject player;

    private bool boolLeft;
    private bool boolRight;

    private bool boolLeftAcc = false;
    private bool boolRightAcc = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        updateLeft();
        updateRight();
        if(boolLeft && boolRight)
        {
            player.GetComponent<ShipBehavior>().gameOver();
        }
    }

    void updateLeft()
    {
        if (leftSide.GetComponent<EnemySpawner>().waveWon && leftSide.activeInHierarchy && boolLeftAcc)
        {
            boolLeftAcc = true;
            boolLeft = true;
        }
    }

    void updateRight()
    {
        if(rightSide.GetComponent<EnemySpawner>().waveWon && rightSide.activeInHierarchy && boolRightAcc)
        {
            boolRightAcc = true;
            boolRight = true;
        }
    }
}

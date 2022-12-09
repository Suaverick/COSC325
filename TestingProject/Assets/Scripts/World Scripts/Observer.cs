using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class Observer : MonoBehaviour
{

    public GameObject left;
    public GameObject right;

    GameObject spaceBoss = null;
    GameObject hellBoss = null;

    bool[] initialState = { true, false };
    bool[] currentState;

    bool boolSpaceBossFound = false;
    bool boolHellBossFound = false;


    private void Start()
    {

    }

    private void Update()
    {

        currentState = getCurrentState();

        if (spaceBoss == null)
        {
            spaceBoss = GameObject.FindGameObjectWithTag("SpaceBoss");
        }
        else
        {
            boolSpaceBossFound = true;
        }

        if (hellBoss == null)
        {
            hellBoss = GameObject.FindGameObjectWithTag("HellBoss");
        }
        else
        {
            boolHellBossFound = true;
        }

        if(!initialState.SequenceEqual(getCurrentState()) && (isLeftActive() != isRightActive()))
        {
            if (initialState[0] && boolSpaceBossFound) updateSpaceBoss();
            if (initialState[1] && boolHellBossFound) updateHellBoss();
            updateInitialState();
        }

    }

    private bool isLeftActive()
    {
        return left.activeInHierarchy;
    }

    private bool isRightActive()
    {
        return right.activeInHierarchy;
    }

    private bool[] getCurrentState()
    {
        bool[] current = { isLeftActive(), isRightActive() };
        return current;
    }

    private void updateSpaceBoss()
    {
        try {
            if (spaceBoss.GetComponent<SpaceBossBehavior>().bool1to2)
            {
                spaceBoss.GetComponent<SpaceBossBehavior>().phase1to2skip();
            }
            else if (spaceBoss.GetComponent<SpaceBossBehavior>().bool2to3)
            {
                spaceBoss.GetComponent<SpaceBossBehavior>().phase2to3skip();
            }
            else if (spaceBoss.GetComponent<SpaceBossBehavior>().boolPhase2)
            {
                spaceBoss.GetComponent<SpaceBossBehavior>().boolPatternOn = false;
            }
            else if (spaceBoss.GetComponent<SpaceBossBehavior>().boolPhase3)
            {
                spaceBoss.GetComponent<SpaceBossBehavior>().boolPatternAndMissleOn = false;
            }
        }
        catch (NullReferenceException e)
        {

        }
    }

    private void updateHellBoss()
    {
        try
        {
            if (hellBoss.GetComponent<HellBossBehavior>().bool1to2)
            {
                hellBoss.GetComponent<HellBossBehavior>().phase1to2skip();
            }
            else if (hellBoss.GetComponent<HellBossBehavior>().bool2to3)
            {
                hellBoss.GetComponent<HellBossBehavior>().phase2to3skip();
            }
        }
        catch (NullReferenceException e)
        {

        }
    }

    private void updateInitialState()
    {
        initialState = currentState;
    }

}

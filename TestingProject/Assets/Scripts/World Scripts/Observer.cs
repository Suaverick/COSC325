using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class Observer : MonoBehaviour
{

    // Game objects for observer
    public GameObject left;
    public GameObject right;

    GameObject spaceBoss = null;
    GameObject hellBoss = null;

    // Bool arrays for seeing the current state of the level
    // { left, right }
    bool[] initialState = { true, false };
    bool[] currentState;

    // Boolean switches for finding the space boss
    bool boolSpaceBossFound = false;
    bool boolHellBossFound = false;

    private void Start()
    {

    }

    private void Update()
    {

        // Get current state of the level
        currentState = getCurrentState();

        // Find the SpaceBoss
        if (spaceBoss == null)
        {
            spaceBoss = GameObject.FindGameObjectWithTag("SpaceBoss");
        }
        else
        {
            boolSpaceBossFound = true;
        }

        // Find the hell boss
        if (hellBoss == null)
        {
            hellBoss = GameObject.FindGameObjectWithTag("HellBoss");
        }
        else
        {
            boolHellBossFound = true;
        }

        // Updates the bosses based on the current state of the level
        if(!initialState.SequenceEqual(getCurrentState()) && (isLeftActive() != isRightActive()))  // If initial state is different from current state, and leftActive and rightActive are not equal
        {
            if (initialState[0] && boolSpaceBossFound) updateSpaceBoss();
            if (initialState[1] && boolHellBossFound) updateHellBoss();
            updateInitialState();
        }

    }

    // Checks if left is currently active in the hierarchy
    private bool isLeftActive()
    {
        return left.activeInHierarchy;
    }

    // Checks if right is current active in the hierarchy
    private bool isRightActive()
    {
        return right.activeInHierarchy;
    }

    // Gets the current state of the level
    private bool[] getCurrentState()
    {
        bool[] current = { isLeftActive(), isRightActive() };
        return current;
    }

    // Updates the states of the SpaceBoss
    private void updateSpaceBoss()
    {
        try {
            if (spaceBoss.GetComponent<SpaceBossBehavior>().bool1to2)  // If phase1to2 transition is active
            {
                spaceBoss.GetComponent<SpaceBossBehavior>().phase1to2skip();     
            }
            else if (spaceBoss.GetComponent<SpaceBossBehavior>().bool2to3)   // If phase1to2 transition is active
            {
                spaceBoss.GetComponent<SpaceBossBehavior>().phase2to3skip();
            }
            else if (spaceBoss.GetComponent<SpaceBossBehavior>().boolPhase2)      // If phase2 is active
            {
                spaceBoss.GetComponent<SpaceBossBehavior>().boolPatternOn = false;      // Reset coroutine
            }
            else if (spaceBoss.GetComponent<SpaceBossBehavior>().boolPhase3)      // If phase3 is active
            {
                spaceBoss.GetComponent<SpaceBossBehavior>().boolPatternAndMissleOn = false;   // Reset coroutine
            }
        }
        catch (NullReferenceException e)
        {
            // Does nothing, just catches the NullReferenceException whenever the player spawps screen
        }
    }

    // Updates the states of the hell boss
    private void updateHellBoss()
    {
        try
        {
            if (hellBoss.GetComponent<HellBossBehavior>().bool1to2)   // If phase1to2 transition is active
            {
                hellBoss.GetComponent<HellBossBehavior>().phase1to2skip();
            }
            else if (hellBoss.GetComponent<HellBossBehavior>().bool2to3)   // If phase2to3 transition is active
            {
                hellBoss.GetComponent<HellBossBehavior>().phase2to3skip();
            }
        }
        catch (NullReferenceException e)
        {
            // Does nothing, just catches the NullReferenceException whenever the player spawps screen
        }
    }

    // Sets the intital state to be equal to the current state
    private void updateInitialState()
    {
        initialState = currentState;
    }

}

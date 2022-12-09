using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEnemyWithGuardBehavior : MonoBehaviour
{

    // Only purpose of this class to handle the Enemy with Guard prefabs
    // If the enenmy and guard prefab inside of the Enemy with guard prefab is dead, then destroy the Enemy with Guard ga object

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount <= 0)
        {
            Destroy(gameObject);
        }
    }
}

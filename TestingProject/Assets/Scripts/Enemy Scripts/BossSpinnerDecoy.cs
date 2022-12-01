using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpinnerDecoy : MonoBehaviour
{

    private float fltTargetScale = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.transform.localScale.y >= fltTargetScale)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, transform.localScale.y - .005f, transform.localScale.z);
        }

    }
}

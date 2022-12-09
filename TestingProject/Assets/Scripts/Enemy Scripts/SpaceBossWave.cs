using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBossWave : MonoBehaviour
{

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Spawns the wave, and increases the size until the scale is greater than 12
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + 0.05f, gameObject.transform.localScale.y + 0.05f, gameObject.transform.localScale.z);
        if(gameObject.transform.localScale.x >= 12)
        {
            Destroy(gameObject);
        }
    }
}

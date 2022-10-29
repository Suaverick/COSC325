using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehavior : MonoBehaviour
{

    Vector2 targetPosition = new Vector2(0, -10);

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // If the background element's position != the target position
        if((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);  // Move toward target position
        }
        else
        {
            transform.position = new Vector2(0, 20);                                                               //Teleport to position (0,20) aka the starting position
        }


    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float fltSpeed;
    private Rigidbody2D rb;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();               // Sets the bullet to be a Ridgid Body (has collision)
        rb.velocity = transform.up * fltSpeed;          // The bullets velocity is set to moving up * the amount of speed given to the bullet
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));    // Gets the bounds of the screen for the scene
    }

    // Update is called once per frame
    void Update()
    {
        // If the position of the bullet is greater than the screens y value, it destroys the bullet
        if(transform.position.y > screenBounds.y)
        {
            Destroy(this.gameObject);
        }
    }
}

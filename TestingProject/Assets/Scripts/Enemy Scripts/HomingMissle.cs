using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissle : MonoBehaviour
{

    public Transform target;
    private Rigidbody2D rb;
    private Vector2 screenBounds;

    public float fltSpeed = 5f;
    public float fltRotateSpeed = 200f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < -screenBounds.y - 2)
        {
            Destroy(this.gameObject);
        }
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, -transform.up).z;
        if (target.transform.position.y < transform.position.y) {
            rb.angularVelocity = -rotateAmount * fltRotateSpeed;
            rb.velocity = -transform.up * fltSpeed;
        }
        else
        {
            rb.angularVelocity = 0;
            rb.velocity = -transform.up * fltSpeed;
        }
    }
}

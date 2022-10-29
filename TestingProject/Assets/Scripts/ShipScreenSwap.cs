using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipScreenSwap : MonoBehaviour
{

    private bool boolLeft = true;

    // When the ship enters a "trigger" box
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Prevents anything from happening when it detects a bullet from entering (temp)
        if (other.gameObject.tag == "Bullet")
        {

        }
        else
        {
            if (boolLeft == true && other.gameObject.tag == "SwapRight")     // If you are on the left screen, and the collision box's tag is SwapRight, change to other scene
            {
                SceneManager.LoadScene("SwappedLevel");
                boolLeft = false;
            }
            if(boolLeft == false && other.gameObject.tag == "SwapLeft")     // If you are on the right screen, and the collision box's tag is SwapLeft, change to other scene
            {
                SceneManager.LoadScene("TestLevel");
                boolLeft = true;
            }
        }
        
    }

}

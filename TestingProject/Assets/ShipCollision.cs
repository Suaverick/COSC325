using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipCollision : MonoBehaviour
{

    private bool boolLeft = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {

        }
        else
        {
            if (boolLeft == true && other.gameObject.tag == "SwapRight")
            {
                SceneManager.LoadScene("SwappedLevel");
                boolLeft = false;
            }
            if(boolLeft == false && other.gameObject.tag == "SwapLeft")
            {
                SceneManager.LoadScene("TestLevel");
                boolLeft = true;
            }
        }
        
    }

}

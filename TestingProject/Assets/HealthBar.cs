using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    // Set player health at beginning of battle
    public void setStartHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    // Call to update health & change health bar appearance 
    public void setHealth(int health)
    {
        slider.value = health;
    }

}

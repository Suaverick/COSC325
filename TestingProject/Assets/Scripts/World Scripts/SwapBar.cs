using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwapBar : MonoBehaviour
{
    public static SwapBar instance;
    public Slider slider;
    public float fillSpeed = 0.05f;
    public float targetProgress = 100; // tweak

    private void Awake()
    {
        instance = this;
        slider = gameObject.GetComponent<Slider>();
    }
 
    void Update()
    {
        if (slider.value < targetProgress)
        {
            slider.value += fillSpeed * Time.deltaTime;
        }
            
    }

    public void IncrementProgress(float newProgress)
    {
        slider.value = slider.value + newProgress;
    }

    public void DecrementProgress(float newProgress)
    {
        slider.value = slider.value - newProgress;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehavior : MonoBehaviour
{
    // Variables needed for BackGroundBeavhior
    public float fltScrollSpeed = 0.5f;
    private float fltOffset;
    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    // All this code does it move the background texture on the plane in the levels
    // Textures are set to repeating, so it will continue to loop
    void Update()
    {
        fltOffset += (Time.deltaTime * fltScrollSpeed) / 2 ;
        mat.SetTextureOffset("_MainTex", new Vector2(0, fltOffset));
    }

}

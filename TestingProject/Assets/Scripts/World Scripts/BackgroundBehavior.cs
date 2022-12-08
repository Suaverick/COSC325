using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehavior : MonoBehaviour
{

    public float fltScrollSpeed = 0.5f;
    private float fltOffset;
    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        fltOffset += (Time.deltaTime * fltScrollSpeed) / 2 ;
        mat.SetTextureOffset("_MainTex", new Vector2(0, fltOffset));
    }

}

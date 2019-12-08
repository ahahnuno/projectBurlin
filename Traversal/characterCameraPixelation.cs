using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterCameraPixelation : MonoBehaviour
{

	//I never used this behaviour, as my flashy post-processing that I've used as an aestetic crutch does not support RenderTexture support -however, it is a pretty cool dynamic pixelation effect -if you've found this, try dropping it on the camera and disabling post-processing -so sweet!

    Camera c;
    RenderTexture r;

    void Awake()
    {
    	
        c = GetComponent<Camera>();
        r = new RenderTexture(512, 288, 16, RenderTextureFormat.ARGB32);
        r.filterMode = FilterMode.Point;
    }

    void OnPreRender()
    {
        c.targetTexture = r;
    }

    void OnPostRender()
    {
        c.targetTexture = null;
        Graphics.DrawTexture(new Rect (0, 0, Screen.width, Screen.height), r);
    }
}

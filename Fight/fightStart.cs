using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fightStart : MonoBehaviour
{

	Camera c;
	AudioSource a;

    void Start()
    {
    	Time.timeScale = 0.05f;
        c = GetComponent<Camera>();
        c.orthographicSize = 0.05f;
        a = GameObject.Find("track").GetComponent<AudioSource>();
        a.pitch = 0.05f;
    }

    void Update()
    {
        Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, Time.deltaTime * 1.5f);
        c.orthographicSize = Mathf.Lerp(c.orthographicSize, 5f, Time.deltaTime * 1.5f);
        a.pitch = Mathf.Lerp(a.pitch,  1f, Time.deltaTime * 1.5f);
    }

}

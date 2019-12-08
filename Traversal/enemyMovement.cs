using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyMovement : MonoBehaviour
{
	UnityEngine.AI.NavMeshAgent n;
	Animator a;
    Transform p;

    Camera c;
	AudioSource au;

    void Start()
    {
    	//initializing stuff
        n = GetComponent<UnityEngine.AI.NavMeshAgent>();
        a = GetComponent<Animator>();
        p = GameObject.Find("character").transform;
        c = GameObject.Find("camera").GetComponent<Camera>();
        au = GameObject.Find("track").GetComponent<AudioSource>();
    }

    void Update()
    {
    	//if the player is in the boss zone -its time to brawl
        if(p.position.z < -45f)
        {
        	//move the navMeshAgent to character
            n.destination = p.position;
        }

		//set animation speed based on movement speed
        a.SetFloat("speed", n.velocity.magnitude * 0.1f);

		//checking if the character are within range
        if(Vector3.Distance(transform.position, p.position) < 5f)
        {
            Invoke("Pop", 1f);
            c.fieldOfView = Mathf.Lerp(c.fieldOfView, 150f, Time.deltaTime * 1f);
            au.pitch = Mathf.Lerp(au.pitch,  1.5f, Time.deltaTime * 1f);
        }
    }

    //load the fight scene
    void Pop()
    {
        SceneManager.LoadScene("fightScene");
    }
}

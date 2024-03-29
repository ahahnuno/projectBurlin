﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class characterHealth : MonoBehaviour
{
    float health = 5f;

    RectTransform r;
    AudioSource a;
    Transform m;

    public void hit()
    {
        //don't let health go below 0 because being undead is a curse
        if(health != 0)
        {
            //take some health off
            health -= 0.75f;

            //if we go below zero, fix that -otherwise the health bar will look dumb
            if(health < 0f)
            {
                health = 0;
            }
        }
    }

    //do the same thing for the smaller lasers but not as much because there are a ton of tiny lasers
    public void halfHit()
    {
        if(health != 0)
        {
            health -= 0.05f;
            if(health < 0f)
            {
                health = 0;
            }
        }
    }

    void Start()
    {
        //initialize stuff
        r = GetComponent<RectTransform>();
        a = GameObject.Find("track").GetComponent<AudioSource>();

        //initialize the enemy or character transform - depending on which side we are on
        if(gameObject.name == "Enemy")
        {
            m = GameObject.Find("enemy").transform;
        }
            else if(gameObject.name == "Character")
        {
            m = GameObject.Find("fightCharacter").transform;
        }
    }

    void Update()
    {

        //transition the health bar size depending on the amount of health we have
        r.localScale = Vector3.Lerp(r.localScale, new Vector3(health * 0.1f, 0.5f, 1f), 5f * Time.deltaTime);

        //if we're almost out of health, start indicating that we're totally getting our butt kicked and the brawl is over
        if(r.localScale.x < 0.01f)
        {

            //spin around because it looks like they're dying in mid air
            m.localEulerAngles = Vector3.Lerp(m.localEulerAngles, new Vector3(0f, -90f, 0f), 5f * Time.deltaTime);

            //slow time down when we die cause it looks cool
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0.01f, 5f * Time.deltaTime);

            //unplug the turn-table, which sound super cool
            a.pitch = Mathf.Lerp(a.pitch, 0f, 5f * Time.deltaTime);

            //if time has almost stopped, reload the traversal scene -because heat death should in fact be a manuverable obstacle in our universe
            Invoke("Pop", 0.5f);
        }
    }

    //forsake this cursed land
    void Pop()
    {
        SceneManager.LoadScene("mainScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserMovement : MonoBehaviour
{

	//I was too lazy to make a different script for the smaller lasers, so we have this for 'half' lasers
    public bool half;

    void Start()
    {
    	//we set our size to zero so we can do a cool bloop to reveal ourselves
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        if(half)
        {
        	//do the cool bloop!
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.5f, 0.5f, 0.5f), 6f * Time.deltaTime);

            //pew-pew to the left!
            transform.Translate(Vector3.right * -20 * Time.deltaTime);

			//if the laser was deflected by the character and is receding back into the enemy domain, take some helath off the enemy health ui bar
            if(transform.position.x > 5)
            {
                GameObject.Find("track").GetComponent<AudioSource>().pitch *= 0.75f;
                GameObject.Find("Enemy").GetComponent<characterHealth>().halfHit();
                Invoke("Die", 0.01f);
            }

        }
        //do all that stuff but bigger and faster since we're a bigger musical note
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(2f, 0.5f, 0.5f), 6f * Time.deltaTime);
            transform.Translate(Vector3.right * -10 * Time.deltaTime);

            if(transform.position.x > 5)
            {
                GameObject.Find("track").GetComponent<AudioSource>().pitch *= 0.5f;
                GameObject.Find("Enemy").GetComponent<characterHealth>().hit();
                Invoke("Die", 0.01f);
            }
		}
    }

	//we hit something! -lets assume we hit the player because there are like no conditions where we do not
	void OnCollisionEnter(Collision collision)
	{
	    if(half)
	    {
	    	//make the music buzz so the player is like ew I got hit yuck - also I am too lazy to make sound effects
            GameObject.Find("track").GetComponent<AudioSource>().pitch *= 1.5f;

            //delete ourselves in one tenth of a second
            Invoke("Die", 0.01f);

			//find the character health bar and take some life off
            GameObject.Find("Character").GetComponent<characterHealth>().halfHit();
	    }
	    //again, do all that stuff but HARDER
	    else
	    {
            GameObject.Find("track").GetComponent<AudioSource>().pitch *= 2f;
            Invoke("Die", 0.1f);
            GameObject.Find("Character").GetComponent<characterHealth>().hit();
        }
	}

	//when we die, we make the audio track not buzz forever, oh and also we die
    void Die()
    {
        GameObject.Find("track").GetComponent<AudioSource>().pitch = 1f;
        Destroy(this.gameObject);
    }
}

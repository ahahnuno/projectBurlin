using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    Animator a;
    CharacterController cc;
    Transform ca;

    void Start()
    {
 		//initializing stuff
        a = transform.GetChild(0).GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        ca = transform.GetChild(1).GetChild(0);

		//lock the cursor so we can pivot around without trouble
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

		//if the player wants to leave, give them their cursor back and pause the game
        if(Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }

		//if the player is holding down space, let them look around (they can not mouse-look usually)
        if(Input.GetButton("Jump"))
        {
            ca.eulerAngles += new Vector3(-Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0) * Time.deltaTime * 60f;
        }
        else
        {
        	//our animations rotate our model for some reason, lets account for it
            ca.localEulerAngles = new Vector3(12f, 0f, 0f);

			//read in mouse and wasd input
		    float hor = Input.GetAxis("Horizontal");
		    float ver = Input.GetAxis("Vertical");
		    float rot = Input.GetAxis("Mouse X");

			//if we're not moveing around, we need to run pivot animations so the totally lame standing-while-spinning does not happen
            if(hor == 0f && ver == 0f)
            {
                a.SetBool("turnable", true);
                if(rot < 0)
                {
                    a.SetFloat("turnX", Mathf.Lerp(a.GetFloat("turnX"), 0f, Time.deltaTime * 10));
                }
                else if(rot > 0)
                {
                    a.SetFloat("turnX", Mathf.Lerp(a.GetFloat("turnX"), 1f, Time.deltaTime * 10));
                }
                else
                {
                    a.SetFloat("turnX", Mathf.Lerp(a.GetFloat("turnX"), 0.5f, Time.deltaTime * 5));
                }
            }
            else
            {
            	//transition the animator back into move mode
                a.SetBool("turnable", false);
            }

			//accounting for framerate and animation speeds
		    Vector3 horTime = transform.right * hor * 5f * Time.deltaTime;
		    Vector3 verTime = transform.forward * ver * 5f * Time.deltaTime;
            Vector3 rotTime = transform.up * rot * 100f * Time.deltaTime;

			//if shift is being held down, set our speed and animation blending to walking level (this was afterthought, so it does not transistion super smooth)
            if(Input.GetKey(KeyCode.LeftShift))
            {
                horTime /= 2f;
                verTime /= 2f;
                a.SetFloat("moveX", hor / 2f);
		        a.SetFloat("moveY", ver / 2f);
            }
            else
            {
            	//if we're not walking, set our animation blending to run level as usual
                a.SetFloat("moveX", hor);
		        a.SetFloat("moveY", ver);
            }

			//use Unity's awesomely simple CharacterController component to move with our adjusted inputs
		    cc.Move(verTime + horTime);
		    //pivot if we're moving the mouse around
		    transform.Rotate(rotTime);

			//if the CharacterController is not on the ground, emulate gravity (there are not cliffs in our game, so we do not really need a falling animation state)
		    if(!cc.isGrounded)
		    {
		        cc.Move(transform.up * -2f * Time.deltaTime);
		    }
		}
    }
}

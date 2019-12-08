using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fightCharacterMovement : MonoBehaviour
{
    void Start()
    {
    	//give the player their cursor back because we need it to parse the character height
        Cursor.lockState = CursorLockMode.None;

		//I tried to make the cursor invisible but it did not work for me, hopefully it will just work in the build
        Cursor.visible = false;
    }

    void Update()
    {
    	//make our fighting character dodge lasers by sticking to the cursor
		transform.position = new Vector3(-5.5f, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - 1f, 0f);

		//the player clicked, things just got real, he's not going down with the laser or dodging it like a buffoon - no he's gonna take that laser and chuck it back at that guy!
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

			//we're doing a raycast to check if he even aimed at the laser right
            if (Physics.Raycast(transform.position + new Vector3(0f, 1.25f, 0f), Vector3.right, out hit))
            {

				//did we aim too early?
                if(hit.distance < 1.5f)
                {
					//we hit it, make it face the opposite direction
                    hit.transform.localEulerAngles = new Vector3(hit.transform.localEulerAngles.x, 180f, hit.transform.localEulerAngles.z);

					//turn our collider off so we don't hit our fellow lasers and make them think we're the player
                    hit.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
    }
}

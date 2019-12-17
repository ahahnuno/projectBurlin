using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyGlitch : MonoBehaviour
{

	//start the jump recursion
    void Start()
    {
		Jump();
    }

	void Jump()
	{
		//reset our position so our random position does not inch us further and further into the void out of view
		transform.position = new Vector3(5.5f, 0f, 0f);

		//click our position into a new random position
		transform.position += new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), Random.Range(-3f, 3f));

		//recurse in one half a second (by the way, Invoke is my favorite think in MonoBehavior -more on this later), the rate at which we jump around is relative to the amount of health we have
		Invoke("Jump", GameObject.Find("Enemy").GetComponent<RectTransform>().localScale.x * 2f + 0.1f);
	}
}

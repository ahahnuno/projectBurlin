using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserSpawn : MonoBehaviour
{
	//bpm and pattern accounts for the fact that we didn't even really need MIDI as my loop was so simple
    public int bpm;
    public float[] pattern;

    GameObject l;
    GameObject lb;
    List<Transform> lukes = new List<Transform>();

    void Start()
    {
    	//getting stuff initialized on startup
		l = GameObject.Find("laser");
		lb = GameObject.Find("laserBass");

		//this begins the recursion on both our beat and bassbeat
        beat();
        bassBeat();
    }

	//i is used to increment through our pattern
	int i = 0;
    void beat()
    {
    	//a laser is spawned from our GameObject template at the pattern specified position
        Instantiate(l, new Vector3(5f, pattern[i % 4], 0f), Quaternion.identity);

		//mono behavior has this nifty method of delaying something without needing ASYNC nor deltaTime implementation -recurs based on our specified BPM
        Invoke("beat", 60f / (bpm * 1f));

        i++;
    }

	//bassbeat is the same, however it recurs 4 times faster than our regular beat, and consequently spawns a less threatening laser
    void bassBeat()
    {
        Instantiate(lb, new Vector3(5f, 0, 0f), Quaternion.identity);
        Invoke("bassBeat", 15f / (bpm * 1));
    }
}

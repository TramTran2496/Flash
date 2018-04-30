using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassControls : General {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		speedupTime += Time.deltaTime;
		if (speedupTime > (colorCycle / speed)){
			speed = increaseSpeed ();
			speedupTime = 0;
		}
		transform.Translate (new Vector3 (0.0f, -Time.deltaTime * speed));

		generateGlasses ();
	}

	void generateGlasses() {
		//TODO at the beginning of a cycle, random type and position of 9 glasses that appear in the cycle
		if (transform.position.y < glassEndAt)
			transform.position = new Vector3 (2, glassStartAt, 0);
	}
}

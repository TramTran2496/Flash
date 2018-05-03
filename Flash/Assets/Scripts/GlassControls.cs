using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassControls : Controls {
	private float glassStartAt = 6.0f;
	private float glassDistance = 6.0f;
	private float glassEndAt = -6.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		speed = increaseSpeed ();
		glassStartAt += Time.deltaTime * speed;
		glassEndAt += Time.deltaTime * speed;

		generateGlasses ();
	}

	void generateGlasses() {
		//TODO at the beginning of a cycle, random type and position of 9 glasses that appear in the cycle
		if (transform.position.y < glassEndAt)
			transform.position = new Vector3 (2, glassStartAt, 4);
	}
}

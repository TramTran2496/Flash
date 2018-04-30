﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassControls : GeneralParameters {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (0.0f, -Time.deltaTime * speed));

		if (transform.position.y < -5)
			transform.position = new Vector3 (0, 10, 0);
	}
}

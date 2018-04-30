using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : GeneralParameters {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y > 0)
			transform.Translate (new Vector3 (0.0f, -Time.deltaTime * speed));
	}
}

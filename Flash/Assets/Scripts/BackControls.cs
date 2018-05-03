using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackControls : Controls {
	
	private Renderer renderer;
	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer>();
		renderer.material.color = Color.cyan;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (0, Time.deltaTime * speed));

		increaseSpeed ();
	}
}

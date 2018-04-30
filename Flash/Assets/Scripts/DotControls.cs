﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotControls : GeneralParameters {
	
	private Renderer renderer;
	public const float colorCycle = 30.0f;

	private float LRmost = 1.5f;
	private int LRsteps = 5;

	void Start () {
		renderer = GetComponent<Renderer>();
		renderer.material.color = Color.cyan;
	}

	void Update () {
		changeColor (Time.fixedTime * speed);

		tappingHandle ();
	}

	void OnCollisionEnter2D (Collision2D coll){
		if (coll.gameObject.tag == "glass")
			Debug.Log ("game over");
	}

	void changeColor(float timesecs) {
		float colorParam = timesecs - (((int)timesecs) / ((int)colorCycle)) * ((int)colorCycle);
		if (((int)timesecs) % (colorCycle * 3) < colorCycle)
			renderer.material.color = Color.Lerp (Color.cyan, Color.yellow, colorParam / colorCycle);
		else if (((int)timesecs) % (colorCycle * 3) < colorCycle * 2)
			renderer.material.color = Color.Lerp (Color.yellow, Color.magenta, colorParam / colorCycle);
		else
			renderer.material.color = Color.Lerp (Color.magenta, Color.cyan, colorParam / colorCycle);
	}

	void tappingHandle(){
		// for mobile
		if (Input.touchCount > 0){
			if (Input.GetTouch (0).position.x < Screen.width / 2 && transform.position.x > -LRmost)
				transform.Translate (new Vector3 (-LRmost / LRsteps, 0.0f));
			else if (Input.GetTouch (0).position.x > Screen.width / 2 && transform.position.x < LRmost)
				transform.Translate (new Vector3 (LRmost / LRsteps, 0.0f));
		}
		else{
			if (transform.position.x < 0){
				if (transform.position.x < -LRmost / LRsteps)
					transform.Translate (new Vector3 (LRmost / LRsteps, 0.0f));
				else
					transform.Translate (new Vector3 (-transform.position.x, 0.0f));
			}
			else if (transform.position.x > 0){
				if (transform.position.x > LRmost / LRsteps)
					transform.Translate (new Vector3 (-LRmost / LRsteps, 0.0f));
				else
					transform.Translate (new Vector3 (-transform.position.x, 0.0f));
			}
		}
		/* for pc testing
		if (Input.GetKey(KeyCode.RightArrow)) {
			if (transform.position.x < LRmost)
				transform.Translate (new Vector3 (LRmost / LRsteps, 0.0f));
		}
		else if (Input.GetKey(KeyCode.LeftArrow)) {
			if(transform.position.x > -LRmost)
				transform.Translate (new Vector3 (-LRmost / LRsteps, 0.0f));
		}
		else {
			if (transform.position.x < 0){
				if (transform.position.x < -LRmost / LRsteps)
					transform.Translate (new Vector3 (LRmost / LRsteps, 0.0f));
				else
					transform.Translate (new Vector3 (-transform.position.x, 0.0f));
			}
			else if (transform.position.x > 0){
				if (transform.position.x > LRmost / LRsteps)
					transform.Translate (new Vector3 (-LRmost / LRsteps, 0.0f));
				else
					transform.Translate (new Vector3 (-transform.position.x, 0.0f));
			}
		}*/
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotControls : MonoBehaviour {
	private Renderer renderer;
	private Rigidbody2D rigidbody;
	public float colorCycle = 5.0f;
	public float speed = 1.0f;
	public float yPos = -6.0f;

	private float LRmost = 2.0f;
	private int LRsteps = 5;

	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();	
		renderer = GetComponent<Renderer>();
		renderer.material.color = Color.cyan;
	}

	void Update () {
		if (rigidbody.gravityScale != 0 && transform.position.y >= -2){
			rigidbody.gravityScale = 0;
			rigidbody.Sleep ();
			yPos = transform.position.y;
		}
		
		float timesecs = Time.fixedTime;
		speed *= (1.0f + Time.deltaTime / 500);
		changeColor (timesecs * speed);

		if (yPos >= -2)
			tappingHandle ();
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotControls : Controls {
	
	private Renderer renderer;

	private float LRmost = 1.5f;
	private int LRsteps = 5;

	void Start () {
		renderer = GetComponent<Renderer>();
		renderer.material.color = Color.cyan;
	}

	void Update () {
		transform.Translate (new Vector3 (0, Time.deltaTime * speed));

		increaseSpeed ();
		changeColor (Time.fixedTime);
		tappingHandle ();
	}

	void OnCollisionEnter2D (Collision2D coll){
		//TODO handle Player meet glass
		if (coll.gameObject.tag == "glass")
			Debug.Log ("game over");
	}

	void changeColor(float timesecs) {
		//TODO change color after meet a glass

		/*float timeInterval = timesecs - ((int)(timesecs / (colorCycle / speed * 3))) * (colorCycle / speed * 3);
		if (timeInterval < ((colorCycle - colorChange) / speed))
			renderer.material.color = Color.cyan;
		else if (timeInterval < (colorCycle / speed)) {
			float colorParam = timeInterval - ((colorCycle - colorChange) / speed);
			renderer.material.color = Color.Lerp (Color.cyan, Color.yellow, colorParam / colorChange);
		}
		else if (timeInterval < ((colorCycle * 2 - colorChange) / speed))
			renderer.material.color = Color.yellow;
		else if (timeInterval < (colorCycle * 2 / speed)) {
			float colorParam = timeInterval - ((colorCycle * 2 - colorChange) / speed);
			renderer.material.color = Color.Lerp (Color.yellow, Color.magenta, colorParam / colorChange);
		}
		else if (timeInterval < ((colorCycle * 3 - colorChange) / speed))
			renderer.material.color = Color.magenta;
		else {
			float colorParam = timeInterval - ((colorCycle * 3 - colorChange) / speed);
			renderer.material.color = Color.Lerp (Color.magenta, Color.cyan, colorParam / colorChange);
		}*/
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotControls : Controls {
	
	private Renderer renderer;
	private TrailRenderer trail;

	private float LRmost = 1.75f;
	private int LRsteps = 3;
	private int colorIdx = 0;
	private const int initialPos = -2;

	void Start () {
		renderer = GetComponent<Renderer>();
		renderer.material.color = Color.cyan;
		trail = GetComponent<TrailRenderer> ();
		trail.material.color = Color.cyan;
	}

	void Update () {
		transform.Translate (new Vector3 (0, Time.deltaTime * speed));
		if(transform.position.y > nextRoundY + initialPos){
			if (transform.position.y - nextRoundY - initialPos <= colorChange)
				changeColor (colorIdx, (transform.position.y - nextRoundY - initialPos) / colorChange);
			else{
				colorIdx++;
				trail.time = trail.time / (speed + 0.5f) * speed;
				speed = increaseSpeed ();
				nextRoundY = toNextRound ();
			}
		}
		tappingHandle ();
	}

	void OnCollisionEnter2D (Collision2D coll){
		//TODO handle Player meet glass
		if (coll.gameObject.tag == "glass")
			Debug.Log ("game over");
	}

	void changeColor(int colorIdx, float colorParam) {
		Color curColor = Color.cyan;
		Color nextColor = Color.cyan;
		switch(colorIdx % 3){
		case 0:
			curColor = Color.cyan;
			nextColor = Color.yellow;
			break;
		case 1:
			curColor = Color.yellow;
			nextColor = Color.magenta;
			break;
		case 2:
			curColor = Color.magenta;
			nextColor = Color.cyan;
			break;
		default:
			curColor = Color.cyan;
			nextColor = Color.cyan;
			break;
		}
		renderer.material.color = Color.Lerp (curColor, nextColor, colorParam);
		trail.material.color = Color.Lerp (curColor, nextColor, colorParam);
	}

	void tappingHandle(){
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
	}
}
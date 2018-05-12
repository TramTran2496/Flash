using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackControls : Controls {
	
	private Renderer renderer;
	private int colorIdx = 0;
	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer>();
		renderer.material.color = Color.cyan;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (0, Time.deltaTime * speed));

		if (transform.position.y > nextRoundY) {
			if (transform.position.y - nextRoundY <= colorChange) {
				changeColor (colorIdx, (transform.position.y - nextRoundY) / (colorChange - 0.5f));
			} else {
				colorIdx++;
				speed = increaseSpeed ();
				nextRoundY = toNextRound ();
			}
		}
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
	}
}

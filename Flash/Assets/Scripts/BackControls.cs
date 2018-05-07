using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackControls : Controls {
	
	private Renderer renderer;
	private int colorIdx = 0;
	public Text score;
	public Text round;
	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer>();
		renderer.material.color = Color.cyan;
		score.text = "Score: 0";
		round.text = "Round 1";
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (0, Time.deltaTime * speed));

		score.text = "Score: " + Mathf.Floor (transform.position.y).ToString ();

		if (transform.position.y <= colorChange)
			showRound (transform.position.y, colorChange / 2);
		else if (transform.position.y > nextRoundY) {
			if (transform.position.y - nextRoundY <= colorChange) {
				changeColor (colorIdx, (transform.position.y - nextRoundY) / colorChange);
				round.text = "Round " + getRound ().ToString ();
				showRound (transform.position.y - nextRoundY, colorChange / 2);
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

	void showRound(float pos, float cycle){
		if (pos <= cycle)
			round.color = Color.Lerp (new Color (1, 1, 1, 0), Color.white, pos / cycle);
		else
			round.color = Color.Lerp (Color.white, new Color (1, 1, 1, 0), (pos - cycle) / cycle);
	}
}

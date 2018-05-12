using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : Controls {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (0, Time.deltaTime * speed));
		float backPos = GameObject.Find ("back").transform.position.y;
		if (transform.position.y - backPos < 0){
			transform.Translate (new Vector3 (transform.position.x, backPos - transform.position.y ));
		}

		if (transform.position.y - nextRoundY > colorChange){
			speed = increaseSpeed ();
			nextRoundY = toNextRound ();
		}
	}
}
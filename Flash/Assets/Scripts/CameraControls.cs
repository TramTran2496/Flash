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

		if (transform.position.y - nextRoundY > colorChange){
			speed = increaseSpeed ();
			nextRoundY = toNextRound ();
		}
	}
}
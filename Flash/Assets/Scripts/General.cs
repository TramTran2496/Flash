using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour {

	public float speed = 3.0f;
	public float speedupTime = 0;

	protected float glassStartAt = 13.0f;
	protected float glassDistance = 6.0f;
	protected float glassEndAt = -5.0f;
	protected const float colorCycle = 18.0f;//depend on glasses initial position, so can't be change
	protected const float colorChange = 2.0f;//depend on glasses initial position, so can't be change

	void Start () {
		
	}

	void Update () {
		
	}

	protected float increaseSpeed() {
		speed *= 1.1f;
		return speed;
	}
}

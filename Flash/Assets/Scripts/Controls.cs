using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	public float speed = 3.0f;

	void Start () {
		
	}

	void Update () {
		
	}

	protected float increaseSpeed() {
		speed *= 1.00005f;
		return speed;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralParameters : MonoBehaviour {

	public float speed = 4.0f;

	void Start () {
		
	}

	void Update () {
		speed *= (1.0f + Time.deltaTime / 1000);
	}
}

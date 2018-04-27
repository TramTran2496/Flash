using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotControls : MonoBehaviour {
	private Renderer renderer;
	private Rigidbody2D rigidbody;
	private float colorCycle = 10.0f;
	public float speed = 1.0f;
	public float yPos = -6.0f;

	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();	
		renderer = GetComponent<Renderer>();
	}

	void Update () {
		if (transform.position.y >= -2){
			rigidbody.Sleep ();
			yPos = transform.position.y;
		}
		
		float timesecs = Time.fixedTime;
		speed *= (1.0f + Time.deltaTime / 1000);
		changeColor (timesecs * speed);

		if (yPos >= -2)
			tappingHandle ();
	}

	void changeColor(float timesecs) {
		float colorParam = timesecs - (((int)timesecs) / ((int)colorCycle)) * ((int)colorCycle);
		if (timesecs <= colorCycle)
			renderer.material.color = Color.Lerp (Color.white, Color.cyan, colorParam / colorCycle);
		else if (((int)timesecs) % (colorCycle * 3) < colorCycle)
			renderer.material.color = Color.Lerp (Color.yellow, Color.cyan, colorParam / colorCycle);
		else if (((int)timesecs) % (colorCycle * 3) < colorCycle * 2)
			renderer.material.color = Color.Lerp (Color.cyan, Color.magenta, colorParam / colorCycle);
		else
			renderer.material.color = Color.Lerp (Color.magenta, Color.yellow, colorParam / colorCycle);
	}

	void tappingHandle(){
		if (Input.touchCount > 0){
			if (Input.GetTouch (0).position.x < Screen.width / 2 && transform.position.x >= -1f)
				transform.Translate (new Vector3 (-0.5f, 0.0f));
			else if (Input.GetTouch (0).position.x > Screen.width / 2 && transform.position.x <= 1f)
				transform.Translate (new Vector3 (0.5f, 0.0f));
		}
		else{
			if (transform.position.x < 0)
				transform.Translate (new Vector3 (0.5f, 0.0f));
			else if (transform.position.x > 0)
				transform.Translate (new Vector3 (-0.5f, 0.0f));
		}
	}
}
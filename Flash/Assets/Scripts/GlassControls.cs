using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlassControls : Controls {

	private const int glassNo = 8;
	public GameObject glass;
	private GameObject[] glasses;

	public Text round;

	private float yStart = 6;
	private float yDistance = 6;
	// Use this for initialization
	void Start () {
		glasses = new GameObject[glassNo];
		generateGlasses ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (0, Time.deltaTime * speed));
		if (transform.position.y - nextRoundY > colorChange){
			speed = increaseSpeed ();
			nextRoundY = toNextRound ();
			yStart += 2 * yDistance;
			generateGlasses ();
		}
	}

	int randomPosX(){
		return Random.Range (0, 3) * 2 - 2;
	}

	Color randomColor(){
		switch (Random.Range (0, 4)){
		case 0:
			return Color.cyan;
		case 1:
			return Color.yellow;
		case 2:
			return Color.magenta;
		default:
			return Color.gray;
		}
	}

	void generateGlasses(){
		//TODO generate obstacles as 4 different levels
		for(int i = 0; i < glassNo; i++) {
			yStart += yDistance;
			Vector3 vt = new Vector3 (randomPosX (), yStart, transform.position.z);
			glasses [i] = (GameObject)Instantiate (glass, vt, Quaternion.identity);
			glasses [i].GetComponent<Renderer> ().material.color = randomColor ();
		}
	}
}

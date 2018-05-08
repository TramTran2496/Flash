using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlassControls : Controls {

	private const int glassNo = 8;
	public GameObject glass;
	private GameObject[] glasses1;
	private GameObject[] glasses2;
	private GameObject[] glasses3;

	public Text round;

	private float yStart = 6;
	private float yDistance = 6;
	// Use this for initialization
	void Start () {
		glasses1 = new GameObject[glassNo];
		glasses2 = new GameObject[glassNo];
		glasses3 = new GameObject[glassNo];
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

	int randomPosXexcept(int except){
		int rand = Random.Range (0, 3) * 2 - 2;
		while (rand == except)
			rand = Random.Range (0, 3) * 2 - 2;
		return rand;
	}

	Color randomColor(int clNum){
		switch (Random.Range (0, clNum)){
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

	int randomLevel(int maxLevel){
		return Random.Range (0, maxLevel);
	}

	void generateGlasses(){
		//TODO generate obstacles as 4 different levels
		for(int i = 0; i < glassNo; i++) {
			yStart += yDistance;
			int x = randomPosXexcept (-3);
			Vector3 vt = new Vector3 (x, yStart, transform.position.z);
			glasses1 [i] = (GameObject)Instantiate (glass, vt, Quaternion.identity);
			glasses1 [i].GetComponent<Renderer> ().material.color = randomColor (4);
			if(getRound () > 2 && randomLevel (2) > 0){
				vt = new Vector3 (randomPosXexcept (x), yStart, transform.position.z);
				glasses2 [i] = (GameObject)Instantiate (glass, vt, Quaternion.identity);
				glasses2 [i].GetComponent<Renderer> ().material.color = randomColor (4);
			}
			else if(getRound () > 3){
				int obsNum = randomLevel (getRound () - 1);
				int x2 = randomPosXexcept (x);
				Color randColor = randomColor (3);
				if(obsNum > 0){
					vt = new Vector3 (x2, yStart, transform.position.z);
					glasses2 [i] = (GameObject)Instantiate (glass, vt, Quaternion.identity);
					while(randColor == glasses1 [i].GetComponent<Renderer> ().material.color)
						randColor = randomColor (3);
					glasses2 [i].GetComponent<Renderer> ().material.color = randColor;
				}
				if(obsNum > 1){
					if(glasses1 [i].GetComponent<Renderer> ().material.color == Color.gray)
						glasses1 [i].GetComponent<Renderer> ().material.color = randomColor (3);
					vt = new Vector3 (-x - x2, yStart, transform.position.z);
					glasses3 [i] = (GameObject)Instantiate (glass, vt, Quaternion.identity);
					Color randColor3 = randomColor (3);
					while(randColor3 == glasses1 [i].GetComponent<Renderer> ().material.color
						|| randColor3 == randColor)
						randColor3 = randomColor (3);
					glasses3 [i].GetComponent<Renderer> ().material.color = randColor3;
				}
			}
		}
	}
}

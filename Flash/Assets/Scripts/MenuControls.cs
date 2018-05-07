using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour {
	private Image tapLine;
	private int frame = 0;
	Color newColor = new Color(1f,1f,1f,1f);

	// Use this for initialization
	void Start () {
		tapLine = GameObject.Find ("tap_to_start").GetComponent<Image>();
		InvokeRepeating ("Blinking", 0.3f, 0.2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Blinking(){
		//Blink image by 4 frame
		switch (frame) {
		case 0:
			newColor = new Color (1f, 1f, 1f, 0.7f);
			frame = 1;
			break;
		case 1:
			newColor = new Color (1f, 1f, 1f, 0.4f);
			frame = 3;
			break;
		case 2:
			newColor = new Color (1f, 1f, 1f, 1f);
			frame = 0;
			break;
		case 3:
			newColor = new Color (1f, 1f, 1f, 0.7f);
			frame = 2;
			break;
		};
		tapLine.color = newColor;
	}

	public void TapEvent(){
		//Stop blinking and change scene
		CancelInvoke ();
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}
}

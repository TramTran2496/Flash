using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialControls : MonoBehaviour {
	private int slideIndex;

	public Button nextBtn, prevBtn;

	// Use this for initialization
	void Start () {
		slideIndex = 0;
		prevBtn.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void goNext(){
		if (slideIndex.Equals(0)) {
			prevBtn.gameObject.SetActive(true);
		} else if (slideIndex.Equals(2)) {
			nextBtn.gameObject.SetActive (false);
		}
		slideIndex++;
		transform.Translate (new Vector3(6.3f,0f));
	}

	public void goPrev(){
		if (slideIndex.Equals(3)) {
			nextBtn.gameObject.SetActive(true);
		} else if (slideIndex.Equals(1)) {
			prevBtn.gameObject.SetActive (false);
		}
		slideIndex--;
		transform.Translate (new Vector3(-6.3f,0f));
	}

	public void exit(){
		SceneManager.LoadScene (0);
	}
}

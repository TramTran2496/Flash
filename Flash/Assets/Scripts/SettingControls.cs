﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingControls : MonoBehaviour {
	private Image buttonImage;

	private Sprite muteSprite;
	private Sprite unmuteSprite;
	private bool mute = false;

	// Use this for initialization
	void Start () {
		buttonImage = GameObject.Find ("MusicButton").GetComponent<Image>();
		muteSprite = Resources.Load<Sprite> ("style01/shape010_style01_color08");
		unmuteSprite = Resources.Load<Sprite>("style01/shape009_style01_color08");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void musicButtonAction() {
		//TODO: handle music setting
		if (mute) {
			buttonImage.sprite = unmuteSprite;
		} else {
			buttonImage.sprite = muteSprite;
		}	
		mute = !mute;
	}

	public void backButtonAction() {
		SceneManager.LoadScene (0);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingControls : MonoBehaviour {
	private Image buttonImage;
	private Sprite muteSprite;
	private Sprite unmuteSprite;
	private MusicClass music;
	private Slider slider;

	private bool mute = false;

	// Use this for initialization
	void Start () {
		buttonImage = GameObject.Find ("MusicButton").GetComponent<Image>();
		muteSprite = Resources.Load<Sprite> ("style01/shape010_style01_color08");
		unmuteSprite = Resources.Load<Sprite>("style01/shape009_style01_color08");
		music = GameObject.Find ("SoundManager").GetComponent<MusicClass> ();
		slider = GameObject.Find ("Slider").GetComponent<Slider> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void musicButtonAction() {
		//TODO: handle music setting
		if (mute) {
			buttonImage.sprite = unmuteSprite;
			music.PlayMusic ();
		} else {
			buttonImage.sprite = muteSprite;
			music.StopMusic ();
		}	
		mute = !mute;
	}

	public void backButtonAction() {
		SceneManager.LoadScene (0);
	}

	public void setMusicVolume() {
		music._audioSource.volume = slider.value;
	}
}

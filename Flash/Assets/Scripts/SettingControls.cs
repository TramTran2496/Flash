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
	private float volume;
	private bool mute = false;

	// Use this for initialization
	void Start () {
		buttonImage = GameObject.Find ("MusicButton").GetComponent<Image>();
		muteSprite = Resources.Load<Sprite> ("style01/shape010_style01_color08");
		unmuteSprite = Resources.Load<Sprite>("style01/shape009_style01_color08");
		music = GameObject.Find ("SoundManager").GetComponent<MusicClass> ();
		slider = GameObject.Find ("Slider").GetComponent<Slider> ();

		volume = PlayerPrefs.GetFloat ("volume", volume);
		slider.value = volume;
		int m = 0;
		mute = PlayerPrefs.GetInt ("mute", m) == 0 ? false : true;
		if (mute) {
			buttonImage.sprite = muteSprite;
			music.StopMusic ();
		} else {
			buttonImage.sprite = unmuteSprite;
			music.PlayMusic ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void musicButtonAction() {
		//TODO: handle music setting
		if (mute) {
			buttonImage.sprite = unmuteSprite;
			PlayerPrefs.SetInt ("mute", 0);
			music.PlayMusic ();
		} else {
			buttonImage.sprite = muteSprite;
			PlayerPrefs.SetInt ("mute", 1);
			music.StopMusic ();
		}
		mute = !mute;
	}

	public void backButtonAction() {
		SceneManager.LoadScene (0);
	}

	public void setMusicVolume() {
		volume = slider.value;
		PlayerPrefs.SetFloat ("volume", volume);
		music._audioSource.volume = volume;
	}
}

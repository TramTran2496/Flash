using UnityEngine;

public class MusicClass : MonoBehaviour
{
	public AudioSource _audioSource;
	public static MusicClass instance = null;
	private float volume;
	private bool mute = false;

	private void Awake()
	{
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
			volume = PlayerPrefs.GetFloat ("volume", volume);
			int m = 0;
			mute = PlayerPrefs.GetInt ("mute", m) == 0 ? false : true;
			if (mute)
				_audioSource.Stop ();
			else
				_audioSource.volume = volume;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	public void PlayMusic()
	{
		if (_audioSource.isPlaying) return;
		_audioSource.Play();
	}

	public void StopMusic()
	{
		_audioSource.Stop();
	}
}
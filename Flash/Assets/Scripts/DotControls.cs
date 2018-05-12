using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DotControls : Controls {
	
	private Renderer renderer;
	private TrailRenderer trail;
	private GameObject gameOverMenu;
	public Text score;
	public Text round;
	public Text bestText;
	private int bestScore;

	private float LRmost = 1.8f;
	private int LRsteps = 2;
	private int colorIdx = 0;
	private const int initialPos = -2;
	private bool isGameOver = false;
	public Button replayBtn, homeBtn;

	void Start () {
		renderer = GetComponent<Renderer>();
		renderer.material.color = Color.cyan;
		trail = GetComponent<TrailRenderer> ();
		trail.material.color = Color.cyan;
		gameOverMenu = GameObject.Find ("GameOver");
		gameOverMenu.SetActive (false);
		score.text = "Score: 0";
		round.text = "Round 1";
		score.color = Color.cyan;
		bestScore = PlayerPrefs.GetInt ("bestscore", bestScore);
		replayBtn.onClick.AddListener(retryButtonAction);
		homeBtn.onClick.AddListener(quitButtonAction);
	}

	void Update () {
		if (!isGameOver){
			transform.Translate (new Vector3 (0, Time.deltaTime * speed));
			float backPos = GameObject.Find ("back").transform.position.y;

			if (transform.position.y - backPos < initialPos){
				transform.Translate (new Vector3 (0.0f, backPos - transform.position.y + initialPos));
			}
			if (backPos <= colorChange)
				showRound (backPos, colorChange / 2);
			else if (transform.position.y > nextRoundY + initialPos){
				if (transform.position.y - nextRoundY - initialPos <= colorChange){
					changeColor (colorIdx, (transform.position.y - nextRoundY - initialPos) / (colorChange - 0.5f));
					round.text = "Round " + getRound ().ToString ();
					showRound (backPos - nextRoundY, colorChange / 2);
				}
				else{
				colorIdx++;
					trail.time = trail.time / (speed + 1) * speed;
					speed = increaseSpeed ();
					nextRoundY = toNextRound ();
				}
			}
			tappingHandle ();
			int scoreNo = (int)backPos;
			score.text = "Score: " + scoreNo.ToString ();
			if (scoreNo > bestScore){
				bestScore = scoreNo;
				PlayerPrefs.SetInt ("bestscore", bestScore);
			}
		}
		bestText.text = "Best Score: " + bestScore;
	}

	void OnCollisionEnter2D (Collision2D coll){
		//TODO handle Player meet glass
		Color collColor = coll.gameObject.GetComponent<Renderer>().material.color;
		if (!renderer.material.color.Equals (collColor)) {
			isGameOver = true;
			trail.time = 0;
			Time.timeScale = 0;
			Text endText = gameOverMenu.transform.Find ("EndScore").GetComponent<Text>();
			endText.color = score.color;
			endText.text = GameObject.Find ("Round").GetComponent<Text>().text + '\n' + score.text;
			gameOverMenu.SetActive (true);
			Debug.Log ("game over");
		} else {
			coll.gameObject.GetComponent<BoxCollider2D> ().isTrigger = true;
		}
	}

	void changeColor(int colorIdx, float colorParam) {
		Color curColor = Color.cyan;
		Color nextColor = Color.cyan;
		switch(colorIdx % 3){
		case 0:
			curColor = Color.cyan;
			nextColor = Color.yellow;
			break;
		case 1:
			curColor = Color.yellow;
			nextColor = Color.magenta;
			break;
		case 2:
			curColor = Color.magenta;
			nextColor = Color.cyan;
			break;
		default:
			curColor = Color.cyan;
			nextColor = Color.cyan;
			break;
		}
		renderer.material.color = Color.Lerp (curColor, nextColor, colorParam);
		trail.material.color = Color.Lerp (curColor, nextColor, colorParam);
		score.color = Color.Lerp (curColor, nextColor, colorParam);
	}

	void showRound(float pos, float cycle){
		if (pos <= cycle)
			round.color = Color.Lerp (new Color (1, 1, 1, 0), Color.white, pos / cycle);
		else
			round.color = Color.Lerp (Color.white, new Color (1, 1, 1, 0), (pos - cycle) / cycle);
	}

	void tappingHandle(){
		if (Input.touchCount > 0){
			if (Input.GetTouch (0).position.x < Screen.width / 2 && transform.position.x > -LRmost){
				if (transform.position.x >= -LRmost * (LRsteps - 1) / LRsteps)
					transform.Translate (new Vector3 (-LRmost / LRsteps, 0.0f));
				else
					transform.Translate (new Vector3 (-LRmost - transform.position.x, 0.0f));
			}
			else if (Input.GetTouch (0).position.x > Screen.width / 2 && transform.position.x < LRmost){
				if (transform.position.x <= LRmost * (LRsteps - 1) / LRsteps)
					transform.Translate (new Vector3 (LRmost / LRsteps, 0.0f));
				else
					transform.Translate (new Vector3 (LRmost - transform.position.x, 0.0f));
			}
		}
		else{
			if (transform.position.x < 0){
				if (transform.position.x < -LRmost / LRsteps)
					transform.Translate (new Vector3 (LRmost / LRsteps, 0.0f));
				else
					transform.Translate (new Vector3 (-transform.position.x, 0.0f));
			}
			else if (transform.position.x > 0){
				if (transform.position.x > LRmost / LRsteps)
					transform.Translate (new Vector3 (-LRmost / LRsteps, 0.0f));
				else
					transform.Translate (new Vector3 (-transform.position.x, 0.0f));
			}
		}
	}

	void move(){
		if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -LRmost){
			if (transform.position.x >= -LRmost * (LRsteps - 1) / LRsteps)
				transform.Translate (new Vector3 (-LRmost / LRsteps, 0.0f));
			else
				transform.Translate (new Vector3 (-LRmost - transform.position.x, 0.0f));
		}
		else if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < LRmost){
			if (transform.position.x <= LRmost * (LRsteps - 1) / LRsteps)
				transform.Translate (new Vector3 (LRmost / LRsteps, 0.0f));
			else
				transform.Translate (new Vector3 (LRmost - transform.position.x, 0.0f));
		}
		else{
			if (transform.position.x < 0){
				if (transform.position.x < -LRmost / LRsteps)
					transform.Translate (new Vector3 (LRmost / LRsteps, 0.0f));
				else
					transform.Translate (new Vector3 (-transform.position.x, 0.0f));
			}
			else if (transform.position.x > 0){
				if (transform.position.x > LRmost / LRsteps)
					transform.Translate (new Vector3 (-LRmost / LRsteps, 0.0f));
				else
					transform.Translate (new Vector3 (-transform.position.x, 0.0f));
			}
		}
	}

	public void retryButtonAction(){
		trail.time = 1;
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	public void quitButtonAction() {
		trail.time = 1;
		Time.timeScale = 1;
		SceneManager.LoadScene (0);
	}
}
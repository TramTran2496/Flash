using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	public float speed = 4.0f;
	public const float colorChange = 4.0f;
	public int nextRoundY = 102;
	private const int roundInterval = 102;

	void Start () {
		
	}

	void Update () {
		
	}

	protected float increaseSpeed() {
		speed += 0.5f;
		return speed;
	}

	protected int toNextRound(){
		nextRoundY += roundInterval;
		return nextRoundY;
	}

	protected int getRound(){
		return nextRoundY / roundInterval + 1;
	}
}

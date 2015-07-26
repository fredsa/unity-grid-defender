using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreTextController : MonoBehaviour {

	int score;
	Text scoreText;
	Animator animator;

	static ScoreTextController instance;

	void Awake() {
		instance = this;
	}
	
	void Start () {
	    scoreText = gameObject.GetComponent<Text> ();
	    animator = gameObject.GetComponent<Animator> ();
		ResetScore ();
	}

	public void ResetScore() {
		score = 0;
		UpdateScore ();
	}

	public static void AddPoints(int points) {
		instance.score += points;
		instance.UpdateScore ();
	}

	void UpdateScore () {
		scoreText.text = string.Format ("{0,-4:n0}", score);
		animator.SetTrigger ("ValueChanged");
	}

}

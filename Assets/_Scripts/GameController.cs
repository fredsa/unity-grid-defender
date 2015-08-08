using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public CanvasTextController scoreTextController;
	public CanvasTextController livesTextController;

	public void Start () {
		scoreTextController.SetValue (0);
		livesTextController.SetValue (3);
	}
	
	public void AddPoints(int points) {
		scoreTextController.IncrementValue (points);
	}

	public int SubtractLife() {
		return livesTextController.IncrementValue (-1);
	}
}

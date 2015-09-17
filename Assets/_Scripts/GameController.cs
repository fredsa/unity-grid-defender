using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;

public class GameController : MonoBehaviour {

	public CanvasTextController scoreTextController;
	public CanvasTextController livesTextController;

	void Start () {
		scoreTextController.SetValue (0);
#if UNITY_EDITOR
		livesTextController.SetValue (2);
#else
		livesTextController.SetValue (3);
#endif
	}

	public void AddPoints(int points) {
		scoreTextController.IncrementValue (points);
	}

	public int SubtractLife() {
		return livesTextController.IncrementValue (-1);
	}

}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;

public class GameController : MonoBehaviour {

#if _DEBUG
	public static bool DEBUG_ENEMY_LIGHTS_ON = true;
#endif

	public CanvasTextController scoreTextController;
	public CanvasTextController livesTextController;

	public void Start () {
		scoreTextController.SetValue (0);
#if UNITY_EDITOR
		livesTextController.SetValue (1);
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

#if _DEBUG
	void UpdateLights(GameObject[] enemies) {
		foreach(GameObject enemy in enemies) {
			Light light = enemy.GetComponentInChildren<Light>();
			if (light) {
				light.enabled = DEBUG_ENEMY_LIGHTS_ON;
//				light.bounceIntensity = DEBUG_ENEMY_LIGHTS_ON ? 8f : 0f;
			}
		}
	}
#endif
	
#if _DEBUG
	void Update() {
		if (Input.GetMouseButtonDown (0)) {
			DEBUG_ENEMY_LIGHTS_ON = !DEBUG_ENEMY_LIGHTS_ON;
			UpdateLights (GameObject.FindGameObjectsWithTag ("Enemy"));
			UpdateLights (GameObject.FindGameObjectsWithTag ("EnemyObstacle"));
		}
	}
#endif

}

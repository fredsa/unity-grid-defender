using UnityEngine;
using System.Collections;

public class KeepOnLoad : MonoBehaviour {

	private static GameObject instance;

	void Awake() {
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = gameObject;
			DontDestroyOnLoad (gameObject);
		}
	}
}

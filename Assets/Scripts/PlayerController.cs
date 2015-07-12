using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Transform grid;

	float speed = .4f;
	float clampX;
	float clampy;

	void Start() {
		clampX = grid.localScale.x/2 - transform.localScale.x/2;
		clampy = grid.localScale.y/2 - transform.localScale.y/2;
	}

	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal") * speed;
		float v = Input.GetAxis ("Vertical") * speed;

		float x = Mathf.Clamp (transform.localPosition.x + h, -clampX, clampX);
		float y = Mathf.Clamp (transform.localPosition.y + v, -clampy, clampy);
		float z = transform.localPosition.z;

		gameObject.transform.localPosition = new Vector3 (x, y, z);
	}
}

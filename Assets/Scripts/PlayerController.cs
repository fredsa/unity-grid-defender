using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	float speed = .15f;

	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal") * speed;
		float v = Input.GetAxis ("Vertical") * speed;

		float x = Mathf.Clamp (transform.localPosition.x + h, -1f, 1f);
		float y = Mathf.Clamp (transform.localPosition.y + v, -1.5f, 1.5f);

		gameObject.transform.localPosition = new Vector3 (x, y, 0);
	}
}

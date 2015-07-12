using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Transform grid;
	public float fingerYOffset = 0f;

	float speed = .4f;
	float clampX;
	float clampy;
	Vector3 targetPosition;
	Plane playerPlane;

	void Start() {
		playerPlane = new Plane(Vector3.forward, transform.position);
		targetPosition = transform.position;
		clampX = grid.localScale.x/2 - transform.localScale.x/2;
		clampy = grid.localScale.y/2 - transform.localScale.y/2;
	}

	void FixedUpdate () {
		if (Input.GetMouseButton (0)) {
			Vector3 pos = Input.mousePosition;
			pos.y = pos.y + fingerYOffset;
			Ray ray = Camera.main.ScreenPointToRay(pos);
			float distance;
			playerPlane.Raycast(ray, out distance);
			targetPosition = ray.GetPoint(distance);

//			Debug.Log (Input.mousePosition);
//			targetPosition = Input.mousePosition;
//			targetPosition.z = 0;
//			targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//			Debug.Log (targetPosition);
//			targetPosition = Input.mousePosition;
			transform.position = targetPosition;
			return;
		}

//		if (Input.touchCount > 0) {
//			targetPosition = Input.GetTouch(0).position;
//			transform.position = targetPosition;
//			return;
//		}

		float x = transform.localPosition.x;
		float y = transform.localPosition.y;

		float h = Input.GetAxis ("Horizontal") * speed;
		float v = Input.GetAxis ("Vertical") * speed;

		x = Mathf.Clamp (x + h, -clampX, clampX);
		y = Mathf.Clamp (y + v, -clampy, clampy);

		float z = transform.localPosition.z;

		gameObject.transform.localPosition = new Vector3 (x, y, z);
	}
}

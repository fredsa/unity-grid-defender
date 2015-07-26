using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerBounds {
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;
}

public class PlayerController : MonoBehaviour {

	public PlayerBounds bounds;
	public Transform grid;
	public float fingerYOffset = 0f;

	float speed = 20f;
	Plane playerPlane;

	void Start() {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		playerPlane = new Plane(Vector3.forward, transform.position);
	}

	void Update() {
		if (Input.GetMouseButtonDown (0) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")) {
			transform.rotation =  Quaternion.LookRotation(-transform.forward, transform.up);
		}

		Vector3 targetPosition;
		if (Input.GetMouseButton (0)) {
			Vector3 pos = Input.mousePosition;
			pos.y = pos.y + fingerYOffset;
			Ray ray = Camera.main.ScreenPointToRay (pos);
			float distance;
			playerPlane.Raycast (ray, out distance);
			targetPosition = ray.GetPoint (distance);
		} else {
			targetPosition = transform.position + new Vector3(Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"), 0f);
		}

		targetPosition = Clamp (targetPosition, bounds);
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);

	}

	Vector3 Clamp (Vector3 pos, PlayerBounds bounds) {
		pos.x = Mathf.Clamp (pos.x, bounds.xMin, bounds.xMax);
		pos.y = Mathf.Clamp (pos.y, bounds.yMin, bounds.yMax);
		return pos;
	}
}

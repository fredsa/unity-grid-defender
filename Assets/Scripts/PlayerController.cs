using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Transform grid;
	public float fingerYOffset = 0f;

	float speed = 50f;
	Vector3 targetPosition;
	Plane playerPlane;
	Rigidbody rb;

	void Start() {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		rb = GetComponent<Rigidbody> ();
		playerPlane = new Plane(Vector3.forward, transform.position);
		targetPosition = transform.position;
	}

	void Update() {
		if (Input.GetMouseButtonDown (0) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")) {
			transform.rotation =  Quaternion.LookRotation(transform.forward, -transform.up);
		}

		Vector3 direction;

		if (Input.GetMouseButton (0)) {
			Vector3 pos = Input.mousePosition;
			pos.y = pos.y + fingerYOffset;
			Ray ray = Camera.main.ScreenPointToRay (pos);
			float distance;
			playerPlane.Raycast (ray, out distance);
			targetPosition = ray.GetPoint (distance);
			direction = targetPosition - transform.position;
		} else {
			direction = new Vector3(Input.GetAxisRaw ("Horizontal"),Input.GetAxisRaw ("Vertical"), 0);
		}

		rb.velocity = direction.normalized * speed;
	}
}

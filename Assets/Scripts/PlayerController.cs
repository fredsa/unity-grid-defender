using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Transform grid;
	public float fingerYOffset = 0f;

	float speed = 30f;
	float clampX;
	float clampy;
	Vector3 targetPosition;
	Plane playerPlane;
	Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		playerPlane = new Plane(Vector3.forward, transform.position);
		targetPosition = transform.position;
		clampX = grid.localScale.x/2 - transform.localScale.x/2;
		clampy = grid.localScale.y/2 - transform.localScale.y/2;
	}

	void FixedUpdate () {

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

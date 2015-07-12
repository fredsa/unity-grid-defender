using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {

	float tumble = 10f;

	Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.angularVelocity = Random.insideUnitSphere * tumble;

		rb.velocity = new Vector3 (-4f, -4f, 0f);
	}

	void FixedUpdate() {
	}
	
}

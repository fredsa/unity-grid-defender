using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {

	float tumble = 10f;

	Rigidbody rb;
//	Vector3 eulerAngles;
//	Quaternion quaternion;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.angularVelocity = Random.insideUnitSphere * tumble;
//		eulerAngles = new Vector3(
//			Random.rotation
//		quaternion = Random.rotation;
	}

	void FixedUpdate() {
//		transform.Rotate (eulerAngles);
//		transform.localRotation = quaternion;
	}
	
}

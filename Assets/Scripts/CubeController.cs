using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {

	float tumble = 10f;

	Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.angularVelocity = Random.insideUnitSphere * tumble;
	}
	
}

using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	float speed = 2f;

	Rigidbody rb;

	void Start () {
	    rb = gameObject.GetComponent<Rigidbody> ();
		rb.velocity = new Vector3 (0, speed, 0);
	}
	
	void Update () {
	}
}

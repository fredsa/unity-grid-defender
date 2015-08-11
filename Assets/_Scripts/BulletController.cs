using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	private float bulletSpeed = 40f;

	private Rigidbody rb;

	void Start () {
	    rb = gameObject.GetComponent<Rigidbody> ();
		rb.velocity = transform.up.normalized * bulletSpeed;
	}

}

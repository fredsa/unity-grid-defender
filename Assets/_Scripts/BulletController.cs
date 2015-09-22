using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	private float bulletSpeed = 40f;

	private Rigidbody2D rb;

	void OnEnable () {
	    rb = gameObject.GetComponent<Rigidbody2D> ();
		rb.velocity = transform.up.normalized * bulletSpeed;
	}

}

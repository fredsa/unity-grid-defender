using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public GameObject explosionPrefab;

	float bulletSpeed = 20f;
	Rigidbody rb;

	void Start () {
	    rb = gameObject.GetComponent<Rigidbody> ();
		rb.velocity = new Vector3 (0, bulletSpeed, 0);
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Enemy")) {
			GameObject clone = Instantiate(explosionPrefab, other.transform.position, Quaternion.identity) as GameObject;
			Destroy(other.gameObject);
		}
	}
}

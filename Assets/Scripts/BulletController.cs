using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public GameObject explosionPrefab;

	float bulletSpeed = 40f;
	Rigidbody rb;

	void Start () {
	    rb = gameObject.GetComponent<Rigidbody> ();
		rb.velocity = transform.up.normalized * bulletSpeed;
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Enemy")) {
			GameObject explosion = Instantiate(explosionPrefab, other.transform.position, Quaternion.identity) as GameObject;
			Color otherColor = other.gameObject.GetComponent<MeshRenderer> ().material.color;

			explosion.GetComponentInChildren<ParticleSystem>().startColor = otherColor;
			explosion.GetComponentInChildren<TextMesh>().color = otherColor;

			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
}

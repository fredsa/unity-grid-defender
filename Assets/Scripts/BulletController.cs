using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public GameObject explosionPrefab;
	public GameObject minePrefab;

	float bulletSpeed = 40f;
	Rigidbody rb;

	void Start () {
	    rb = gameObject.GetComponent<Rigidbody> ();
		rb.velocity = transform.up.normalized * bulletSpeed;
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Enemy")) {
			Color otherColor = other.gameObject.GetComponent<MeshRenderer> ().material.color;

			GameObject explosion = Instantiate(explosionPrefab, other.transform.position, Quaternion.identity) as GameObject;
			GameObject mine = Instantiate(minePrefab, other.transform.position, Quaternion.identity) as GameObject;

			explosion.GetComponentInChildren<ParticleSystem>().startColor = otherColor;
			explosion.GetComponentInChildren<TextMesh>().color = otherColor;
			mine.GetComponentInChildren<MeshRenderer>().material.color = otherColor;
			mine.GetComponent<Rigidbody>().angularVelocity = other.gameObject.GetComponent<Rigidbody>().angularVelocity / 5f;

			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
}

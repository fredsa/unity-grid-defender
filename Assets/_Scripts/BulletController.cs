using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public GameObject explosionPrefab;
	public GameObject minePrefab;

	private float bulletSpeed = 40f;
	private int hitPoints = 3;

	private Rigidbody rb;

	void Start () {
	    rb = gameObject.GetComponent<Rigidbody> ();
		rb.velocity = transform.up.normalized * bulletSpeed;
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Enemy") || other.CompareTag ("EnemyObstacle")) {
			int points = 10;
			Color otherColor = other.gameObject.GetComponent<MeshRenderer> ().material.color;

			GameObject explosion = Instantiate(explosionPrefab, other.transform.position, Quaternion.identity) as GameObject;
			explosion.GetComponentInChildren<ParticleSystem>().startColor = otherColor;
			explosion.GetComponentInChildren<TextMesh>().color = otherColor;
			explosion.GetComponentInChildren<TextMesh>().text = string.Format("{0}", points);

			if (other.CompareTag ("Enemy")) {
				GameObject mine = Instantiate(minePrefab, other.transform.position, Quaternion.identity) as GameObject;
				otherColor *= .8f;
				mine.GetComponentInChildren<MeshRenderer>().material.color = otherColor;
			}
			
			Destroy(other.gameObject);
			ScoreTextController.AddPoints(points);
			if (--hitPoints <= 0) {
				Destroy(gameObject);
			}
		}
	}
}

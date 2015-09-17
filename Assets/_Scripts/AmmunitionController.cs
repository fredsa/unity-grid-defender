using UnityEngine;
using System.Collections;

public class AmmunitionController : MonoBehaviour {

	public GameObject explosionPrefab;
	public GameObject minePrefab;
	public int hitPoints = 3;

	private float minTimeBetweenExplosions = .01f;
	private static float nextAllowedExplosionTime = 0f;
	private GameController gameController;

	void Start() {
		gameController = FindObjectOfType<GameController> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Assert(other.CompareTag ("Enemy") || other.CompareTag ("Enemy Obstacle") || other.CompareTag ("Worldbox"), other.gameObject.name);
		if (other.CompareTag ("Worldbox")) {
			return;
		}
		int points = 10;
		Color otherColor = other.transform.GetChild(0).gameObject.GetComponent<MeshRenderer> ().material.color;
		otherColor.a = 1f;

		if (Time.time > nextAllowedExplosionTime) {
			nextAllowedExplosionTime = Time.time + minTimeBetweenExplosions;
			GameObject explosion = Instantiate(explosionPrefab, other.transform.position, Quaternion.identity) as GameObject;
			explosion.GetComponentInChildren<ParticleSystem>().startColor = otherColor;
			explosion.GetComponentInChildren<TextMesh>().color = otherColor;
			explosion.GetComponentInChildren<TextMesh>().text = string.Format(Constants.pointsFormat, points);
		}
		
		if (other.CompareTag ("Enemy")) {
			GameObject mine = Instantiate(minePrefab, other.transform.position, Quaternion.identity) as GameObject;
			mine.transform.parent = other.transform.parent;
			otherColor *= .8f;
			mine.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = otherColor;
			mine.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = otherColor;
		}
		
		Destroy(other.gameObject);
		gameController.AddPoints(points);
		if (--hitPoints <= 0) {
			Destroy(gameObject);
		}
	}

}

using UnityEngine;
using System.Collections;

public class AmmunitionController : MonoBehaviour {

	public GameObject explosionPrefab;
	public GameObject minePrefab;

	public int hitPoints = 3;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Enemy") || other.CompareTag ("EnemyObstacle")) {
			int points = 10;
			Color otherColor = other.gameObject.GetComponent<MeshRenderer> ().material.color;
			otherColor.a = 1f;
			
			GameObject explosion = Instantiate(explosionPrefab, other.transform.position, Quaternion.identity) as GameObject;
			explosion.GetComponentInChildren<ParticleSystem>().startColor = otherColor;
			explosion.GetComponentInChildren<TextMesh>().color = otherColor;
			explosion.GetComponentInChildren<TextMesh>().text = string.Format("{0}", points);
			
			if (other.CompareTag ("Enemy")) {
				GameObject mine = Instantiate(minePrefab, other.transform.position, Quaternion.identity) as GameObject;
				mine.transform.parent = other.transform.parent;
				otherColor *= .8f;
				mine.GetComponentInChildren<MeshRenderer>().material.color = otherColor;
				mine.GetComponentInChildren<Light>().color = otherColor;
			}
			
			Destroy(other.gameObject);
			FindObjectOfType<GameController>().AddPoints(points);
			if (--hitPoints <= 0) {
				Destroy(gameObject);
			}
		}
	}

}

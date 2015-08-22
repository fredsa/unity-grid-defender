using UnityEngine;
using System.Collections;

public class CapsuleController : MonoBehaviour {

	public GameObject pointsEarnedPrefab;

	private int points = 1000;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			GameObject pointsEarned = Instantiate(pointsEarnedPrefab, transform.position, Quaternion.identity) as GameObject;
			var color = GetComponentInChildren<MeshRenderer> ().material.color;
			other.GetComponent<PlayerController>().SetBonusColor(color);
			pointsEarned.GetComponent<TextMesh> ().color = color;
			pointsEarned.GetComponent<TextMesh>().text = string.Format(Constants.pointsFormat, points);
			FindObjectOfType<GameController>().AddPoints(points);
			GetComponentInChildren<DetachPlaySelfDestructController>().DetachPlaySelfDestruct();
			Destroy(gameObject);
		}
	}
}

using UnityEngine;
using System.Collections;

public class CapsuleController : MonoBehaviour {

	public GameObject pointsEarnedPrefab;
	public int bonusId;

	private GameController gameController;
	private int points = 1000;

	void Start() {
		gameController = FindObjectOfType<GameController> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
#if UNITY_EDITOR
		Debug.Assert(other.CompareTag ("Player"), other.gameObject.name);
#endif
		GameObject pointsEarned = Instantiate(pointsEarnedPrefab, transform.position, Quaternion.identity) as GameObject;
		var color = GetComponentInChildren<MeshRenderer> ().material.color;
		other.GetComponent<PlayerController>().SetBonus(bonusId);
		pointsEarned.GetComponent<TextMesh> ().color = color;
		pointsEarned.GetComponent<TextMesh>().text = string.Format(Constants.pointsFormat, points);
		gameController.AddPoints(points);
		GetComponentInChildren<DetachPlaySelfDestructController>().DetachPlaySelfDestruct();
		Destroy(gameObject);
	}
}

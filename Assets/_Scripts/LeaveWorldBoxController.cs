using UnityEngine;
using System.Collections;

public class LeaveWorldBoxController : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other) {
		Debug.Assert(other.CompareTag ("Enemy") || other.CompareTag ("Enemy Obstacle") || other.CompareTag("Player Bullet"), other.gameObject.name);
		Destroy(other.gameObject);
	}
}

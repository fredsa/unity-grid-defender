using UnityEngine;
using System.Collections;

public class LeaveWorldBoxController : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other) {
		Destroy(other.gameObject);
	}
}

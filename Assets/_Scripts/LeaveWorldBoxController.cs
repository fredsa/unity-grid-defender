using UnityEngine;
using System.Collections;

public class LeaveWorldBoxController : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		Destroy(other.gameObject);
	}
}

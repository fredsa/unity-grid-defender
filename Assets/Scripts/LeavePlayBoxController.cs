using UnityEngine;
using System.Collections;

public class LeavePlayBoxController : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag ("Bullet")) {
			Destroy(other.gameObject);
			return;
		}

		if (other.gameObject.CompareTag ("Enemy")) {
			other.gameObject.GetComponent<ChainController>().UTurn();
			return;
		}
	}
}

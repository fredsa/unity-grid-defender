using UnityEngine;
using System.Collections;

public class LeavePlayBoxController : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag ("Bullet")) {
			other.transform.localPosition = new Vector3(0,0,0);
		}
	}
}

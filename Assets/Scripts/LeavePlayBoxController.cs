﻿using UnityEngine;
using System.Collections;

public class LeavePlayBoxController : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		if (!other.gameObject.CompareTag ("Player")) {
			Destroy(other.gameObject);
		}
	}
}

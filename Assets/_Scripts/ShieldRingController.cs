using UnityEngine;
using System.Collections;

public class ShieldRingController : MonoBehaviour {

	void Start () {
		GetComponent<Rigidbody> ().angularVelocity = transform.up * 4f;
		transform.rotation = Quaternion.LookRotation (new Vector3(1f, 0.3f, 0f));
	}

}

using UnityEngine;
using System.Collections;

public class RandomRotatorController : MonoBehaviour {

	void Start () {
		GetComponent<Rigidbody> ().angularVelocity = Random.rotation.eulerAngles * 5f;
	}
	
}

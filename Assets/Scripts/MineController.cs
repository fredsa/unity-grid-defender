using UnityEngine;
using System.Collections;

public class MineController : MonoBehaviour {

	float speed = 10f;
	bool isDropping = false;

	void Start () {
		Invoke ("Drop", 2f);
	}

	void Drop() {
		isDropping = true;
//		GetComponent<Animator> ().SetTrigger ("Drop");
	}

	void FixedUpdate() {
		if (isDropping) {
			transform.position += new Vector3 (0f, -speed * Time.deltaTime, 0f);
		}
	}
}

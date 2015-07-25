using UnityEngine;
using System.Collections;

public class ChainController : MonoBehaviour {

	private GameObject target;
	private float retargetTime;
	private Vector3 targetPosition;
	private Rigidbody rb;
	private Vector3 velocity;
	private int turnCount;

	public void Setup (GameObject target, Vector3 velocity) {
		this.target = target;
		this.velocity = velocity;
		if (!target) {
			rb.velocity = velocity;
		}
	}

	public void UTurn() {
		if (target) {
			return;
		}
		rb.velocity = Quaternion.Euler(0, 0, 180) * rb.velocity;
		retargetTime += 2f;
	}

	void Awake() {
		rb = GetComponent<Rigidbody> ();
	}
	
	void Start () {
		retargetTime = Time.time;
		targetPosition = transform.position;
	}

	void Turn() {
		rb.velocity = Quaternion.Euler(0, 0, 90 * turnCount) * velocity;
		turnCount++;
	}

	void Update () {
		if (target) {
			transform.position = Vector3.MoveTowards (transform.position, targetPosition, velocity.magnitude * Time.deltaTime);
		}

		if (Time.time >= retargetTime) {

			if (target) {
				retargetTime += .2f;
				targetPosition = target.transform.position;
			} else {
				retargetTime += 2f;
				Turn ();
			}
		}
	}
}

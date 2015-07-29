using UnityEngine;
using System.Collections;

public class ChainController : MonoBehaviour {

	public float timeToFirstHeadMove = 2f;
	public float timeBetweenHeadMoves = 1f;

	private float desiredDistance = .6f;
	private float timeNoMoveAfterUturn = .5f;
	private GameObject target;
	private float retargetTime;
	private Vector3 velocity;
	private int index;

	public void Setup (GameObject target, Vector3 velocity) {
		this.target = target;
		this.velocity = velocity;
	}

	public void SetIndex (int index) {
		this.index = index;
	}

	public void UTurn() {
		if (target) {
			return;
		}
		Turn (180);
		retargetTime = Time.time + timeNoMoveAfterUturn;
	}

	void Start () {
		if (target) {
			retargetTime = Time.time;
		} else {
			retargetTime = Time.time + timeToFirstHeadMove;
		}
	}

	void Turn(float degrees) {
		velocity = Quaternion.Euler(0, 0, degrees) * velocity;
	}

	void FixedUpdate () {
		if (target) {
			Vector3 diff = target.transform.position - transform.position;
			if (diff.magnitude > desiredDistance) {
				transform.position += diff * (diff.magnitude - desiredDistance);
			}
		} else {
			transform.position += velocity * Time.deltaTime;
			if (Time.time >= retargetTime) {
				retargetTime += timeBetweenHeadMoves;
				Turn (90 * Mathf.RoundToInt(Random.Range(-1f, 1f)));
			}
		}
	}
}

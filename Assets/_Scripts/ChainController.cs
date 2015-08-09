using UnityEngine;
using System.Collections;

public class ChainController : MonoBehaviour {
	
	public GameObject playbox;
	float timeToFirstHeadMove = 1f;
	float timeBetweenHeadMoves = .7f;

	private float desiredDistance = .6f;
	private GameObject target;
	private float retargetTime;
	private Vector3 velocity;
	private float velocityMagnitude;
	private int index;
	private float xMax;
	private float yMax;

	public void Setup (GameObject playbox, GameObject target, Vector3 velocity) {
		this.playbox = playbox;
		this.target = target;
		this.velocity = velocity;
		velocityMagnitude = velocity.magnitude;
		xMax = playbox.transform.localScale.x / 2;
		yMax = playbox.transform.localScale.y / 2;
	}

	public void SetIndex (int index) {
		this.index = index;
	}

	void Start () {
		if (target) {
			retargetTime = Time.time;
		} else {
			transform.position = CalculateNewPos();
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
				transform.position = target.transform.position - (diff.normalized * desiredDistance);
			}
		} else {
			var newPos = CalculateNewPos ();
			if (Time.time >= retargetTime) {
				Turn (90 * Mathf.RoundToInt (Random.Range (-1f, 1f)));
				retargetTime = Time.time + timeBetweenHeadMoves;
			}
			transform.position = newPos;
		}
	}

	Vector3 CalculateNewPos () {
		Vector3 newPos = transform.position + velocity * Time.deltaTime;
		if (newPos.x < -xMax) {
			velocity.x = velocityMagnitude;
			retargetTime = Time.time + .1f;
		} else if (newPos.x > xMax) {
			velocity.x = -velocityMagnitude;
			retargetTime = Time.time + .1f;
		}
		if (newPos.y < -yMax) {
			velocity.y = velocityMagnitude;
			retargetTime = Time.time + .1f;
		} else if (newPos.y > yMax) {
			velocity.y = -velocityMagnitude;
			retargetTime = Time.time + .1f;
		}
		return newPos;
	}

	float RandomOneOrNegativeOne () {
		return Random.value > .5f ? 1 : -1;
	}
}

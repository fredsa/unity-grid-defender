using UnityEngine;
using System.Collections;

public class ChainController : MonoBehaviour {
	
	public GameObject playbox;
	public float timeToFirstHeadMove = 2f;
	public float timeBetweenHeadMoves = 1f;

	private float desiredDistance = .6f;
	private GameObject target;
	private float retargetTime;
	private Vector3 velocity;
	private int index;

	public void Setup (GameObject playbox, GameObject target, Vector3 velocity) {
		this.playbox = playbox;
		this.target = target;
		this.velocity = velocity;
	}

	public void SetIndex (int index) {
		this.index = index;
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
				transform.position = target.transform.position - (diff.normalized * desiredDistance);
			}
		} else {
			Vector3 newPos = transform.position + velocity * Time.deltaTime;
			bool clamped = false;
			clamped |= ClampAndMaybeTurn(newPos.x, -playbox.transform.localScale.x / 2, playbox.transform.localScale.x / 2, out newPos.x);
			clamped |= ClampAndMaybeTurn(newPos.y, -playbox.transform.localScale.y / 2, playbox.transform.localScale.y / 2, out newPos.y);
			if (clamped || Time.time >= retargetTime) {
				retargetTime += timeBetweenHeadMoves;
				Turn (90 * Mathf.RoundToInt (Random.Range (-1f, 1f)));
			}
			transform.position = newPos;
		}
	}

	bool ClampAndMaybeTurn (float value, float min, float max, out float newValue) {
		if (value < min) {
			newValue = min;
			return true;
		} else if (value > max) {
			newValue = max;
			return true;
		} else {
			newValue = value;
			return false;
		}
	}
}

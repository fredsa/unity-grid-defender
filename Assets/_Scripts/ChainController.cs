using UnityEngine;
using System.Collections;

public class ChainController : MonoBehaviour {
	
	float timeToFirstHeadMove = 1f;
	float timeBetweenHeadMoves = .8f;

	private bool hasEnteredPlaybox;
	private float desiredDistance = .6f;
	private GameObject target;
	private float retargetTime;
	private Vector3 velocity;
	private float velocityMagnitude;
	private float xMax;
	private float yMax;
	private bool glowActivate = true;

	public void Setup (GameObject playbox, GameObject target, Vector3 velocity) {
		this.target = target;
		hasEnteredPlaybox = target != null;
		this.velocity = velocity;
		velocityMagnitude = velocity.magnitude;
		xMax = playbox.transform.localScale.x / 2;// - velocityMagnitude * Time.fixedDeltaTime;
		yMax = playbox.transform.localScale.y / 2;// - velocityMagnitude * Time.fixedDeltaTime;
	}

	void SetGlowActive(bool glowActivate) {
		transform.GetChild(1).gameObject.SetActive(glowActivate);
		this.glowActivate = glowActivate;
	}

	void Start () {
		if (target) {
			retargetTime = Time.time;
			SetGlowActive(false);
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
				if (!glowActivate) {
					SetGlowActive(true);
				}
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
		if (!hasEnteredPlaybox && newPos.x >= -xMax && newPos.x <= xMax && newPos.y >= -yMax && newPos.y <= yMax) {
			hasEnteredPlaybox = true;
		}
		if (hasEnteredPlaybox) {
			if (newPos.x < -xMax) {
				newPos.x = -xMax;
				velocity.x = velocityMagnitude;
				velocity.y = velocityMagnitude * Random.Range(-1f, 1f);
			} else if (newPos.x > xMax) {
				newPos.x = xMax;
				velocity.x = -velocityMagnitude;
				velocity.y = velocityMagnitude * Random.Range(-1f, 1f);
			}
			if (newPos.y < -yMax) {
				newPos.y = -yMax;
				velocity.x = velocityMagnitude * Random.Range(-1f, 1f);
				velocity.y = velocityMagnitude;
			} else if (newPos.y > yMax) {
				newPos.y = yMax;
				velocity.x = velocityMagnitude * Random.Range(-1f, 1f);
				velocity.y = -velocityMagnitude;
			}
		}
		return newPos;
	}

	float RandomOneOrNegativeOne () {
		return Random.value > .5f ? 1 : -1;
	}
}

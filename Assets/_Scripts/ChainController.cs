using UnityEngine;
using System.Collections;

public class ChainController : MonoBehaviour {

	public float timeToFirstHeadMove = 2f;
	public float timeBetweenHeadMoves = 1f;

	float tailFollowRate = .1f;
	float timeNoMoveAfterUturn = .5f;

	private GameObject target;
	private float retargetTime;
	private Vector3 targetPosition;
	private Vector3 velocity;

	public void Setup (GameObject target, Vector3 velocity) {
		this.target = target;
		this.velocity = velocity;
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
			targetPosition = transform.position;
		} else {
			retargetTime = Time.time + timeToFirstHeadMove;
		}
	}

	void Turn(float degrees) {
		velocity = Quaternion.Euler(0, 0, degrees) * velocity;
	}

	void Update () {
		if (target) {
			transform.position = Vector3.MoveTowards (transform.position, targetPosition, velocity.magnitude * Time.deltaTime);
		} else {
			transform.position += velocity * Time.deltaTime;
		}

		if (Time.time >= retargetTime) {
			if (target) {
				retargetTime += tailFollowRate;
				targetPosition = target.transform.position;
			} else {
				retargetTime += timeBetweenHeadMoves;
				Turn (90 * Mathf.RoundToInt(Random.Range(-1f, 1f)));
			}
		}
	}
}

﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerBounds {
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;
}

public class PlayerController : MonoBehaviour {
	
	public GameObject explosionPrefab;
	public PlayerBounds bounds;
	public Transform grid;
	public bool invinsible = false;

	private GameObject playerCapsule;
	private Material playerCapsuleMaterial;
	private float fingerYOffset = 2f;
	private float maxTrackSpeed = 40f;
	private float keyboardSpeedMultiplier = .4f;
	private Plane playerPlane;
	private Animator animator;
	private int PlayerDeathProperty = Animator.StringToHash ("PlayerDeath");
	private Color bonusColor;

	public void SetBonusColor(Color bonusColor) {
		this.bonusColor = bonusColor;
	}

	void Start() {
		playerCapsule = transform.GetChild (0).gameObject;
		playerCapsuleMaterial = playerCapsule.GetComponent<MeshRenderer> ().material;
		bonusColor = playerCapsuleMaterial.color;
		animator = gameObject.GetComponent<Animator> ();
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		playerPlane = new Plane(Vector3.forward, transform.position);
	}

	void Update() {
		playerCapsuleMaterial.color = bonusColor;
		Vector3 targetPosition;
		if (Input.GetMouseButton (0)) {
			Vector3 pos = Input.mousePosition;
			Ray ray = Camera.main.ScreenPointToRay (pos);
			float distance;
			playerPlane.Raycast (ray, out distance);
			targetPosition = ray.GetPoint (distance);
			targetPosition.y += fingerYOffset;
		} else {
			targetPosition = transform.position + new Vector3(Input.GetAxisRaw ("Horizontal") * keyboardSpeedMultiplier, Input.GetAxisRaw ("Vertical") * keyboardSpeedMultiplier, 0f);
		}

		targetPosition = Clamp (targetPosition, bounds);
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, maxTrackSpeed * Time.deltaTime);

	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Enemy") || other.gameObject.CompareTag ("EnemyObstacle")) {
			Destroy(other.gameObject);
			Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			if (!invinsible) {
				FindObjectOfType<GameController>().SubtractLife();
				animator.SetTrigger (PlayerDeathProperty);
			}
		}
	}

	private Vector3 Clamp (Vector3 pos, PlayerBounds bounds) {
		pos.x = Mathf.Clamp (pos.x, bounds.xMin, bounds.xMax);
		pos.y = Mathf.Clamp (pos.y, bounds.yMin, bounds.yMax);
		return pos;
	}
}

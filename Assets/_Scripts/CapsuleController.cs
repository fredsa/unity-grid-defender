﻿using UnityEngine;
using System.Collections;

public class CapsuleController : MonoBehaviour {

	public GameObject pointsEarnedPrefab;

	private int points = 1000;

	void Start () {
		GetComponent<Rigidbody> ().angularVelocity = Random.rotation.eulerAngles;
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			GameObject pointsEarned = Instantiate(pointsEarnedPrefab, transform.position, Quaternion.identity) as GameObject;
			pointsEarned.GetComponent<TextMesh>().color = GetComponentInChildren<MeshRenderer>().material.color;
			pointsEarned.GetComponent<TextMesh>().text = string.Format(Constants.pointsFormat, points);
			FindObjectOfType<GameController>().AddPoints(points);
			AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position);
			Destroy(gameObject);
		}
	}
}

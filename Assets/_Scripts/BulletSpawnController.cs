using UnityEngine;
using System.Collections;

public class BulletSpawnController : MonoBehaviour {

	public GameObject bulletPrefab;
	public float rate = .1f;

	private float nextShotTime = 0f;

	void Update () {
		if (Time.time < nextShotTime) {
			return;
		}
		nextShotTime = Time.time + rate;
		Instantiate (bulletPrefab, transform.position, transform.rotation);
	}
}

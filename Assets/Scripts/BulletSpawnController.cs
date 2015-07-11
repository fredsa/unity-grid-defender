using UnityEngine;
using System.Collections;

public class BulletSpawnController : MonoBehaviour {

	public GameObject bulletPrefab;

	float rate = .1f;
	float nextShotTime = 0f;

	void Start () {
	
	}
	
	void FixedUpdate () {
		if (Time.time < nextShotTime) {
			return;
		}
		nextShotTime = Time.time + rate;
		GameObject bullet = Instantiate (bulletPrefab, transform.position, transform.rotation) as GameObject;
//		bullet.transform.parent = gameObject.transform;
	}
}

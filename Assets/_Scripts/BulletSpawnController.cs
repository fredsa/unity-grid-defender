using UnityEngine;
using System.Collections;

public class BulletSpawnController : MonoBehaviour {

	public GameObject bulletPrefab;
	public float rate = .15f;

	private float nextShotTime = 0f;
	private int bulletCount = 1;
	private int[] bulletAngles = new int[] {0};

	public void SetBulletCount(int bulletCount) {
		this.bulletCount = bulletCount;
	}

	public void SetBulletAngles(int[] bulletAngles) {
		this.bulletAngles = bulletAngles;
	}

	void Update () {
		if (!Input.GetMouseButton (0)) {
			return;
		}
		if (Time.time < nextShotTime) {
			return;
		}
		nextShotTime = Time.time + rate / bulletCount;
		for (int i=0; i<bulletAngles.Length; i++) {
			Quaternion rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, bulletAngles[i]));
           	Instantiate (bulletPrefab, transform.position, rotation);
		}
	}
}

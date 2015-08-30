using UnityEngine;
using System.Collections;

public class BulletSpawnController : MonoBehaviour {

	public GameObject bulletPrefab;
	public float rate = .15f;

	private float resetTime = 5f;
#if _DEBUG
#else
	private int defaultBulletCount = 1;
	private int[] defaultBulletAngles = new int[] {0};
#endif

	private float nextShotTime = 0f;
	private int bulletCount;
	private int[] bulletAngles;

	void Awake() {
		Reset ();
	}

	public void Reset() {
#if _DEBUG
#else
		bulletCount = defaultBulletCount;
		bulletAngles = defaultBulletAngles;
#endif
	}

	public void SetBulletCount(int bulletCount) {
		this.bulletCount = bulletCount;
		Invoke ("Reset", resetTime);
	}

	public void SetBulletAngles(int[] bulletAngles) {
		this.bulletAngles = bulletAngles;
		Invoke ("Reset", resetTime);
	}

	void Update () {
#if _DEBUG
#else
		if (!Input.GetButton("Fire1")) {
			return;
		}
#endif
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

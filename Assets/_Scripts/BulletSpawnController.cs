using UnityEngine;
using System.Collections;

public class BulletSpawnController : MonoBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletHolder;
	public float rate = .15f;

#if _DEBUG
	private int defaultBulletCount = 2;
	private int[] defaultBulletAngles = new int[] {-3, 3};
#else
	private int defaultBulletCount = 1;
	private int[] defaultBulletAngles = new int[] {0};
#endif

	private float nextShotTime = 0f;
	private int bulletCount;
	private int[] bulletAngles;
	private static int MAX_BULLETS = 500;
	private static GameObject[] bullets = new GameObject[MAX_BULLETS];

	void Start() {
		for (int i=0; i<MAX_BULLETS; i++) {
			bullets[i] = Instantiate (bulletPrefab);
			bullets[i].name += " " + i;
			bullets[i].transform.SetParent(bulletHolder);
			bullets[i].SetActive(false);
		}
	}

	public void SetBulletCount(int bulletCount) {
		this.bulletCount = bulletCount;
	}

	public void SetBulletAngles(int[] bulletAngles) {
		this.bulletAngles = bulletAngles;
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
		int bulletindex = 0;
		for (int i=0; i<bulletAngles.Length; i++) {
			Quaternion rotation = Quaternion.Euler (transform.rotation.eulerAngles + new Vector3 (0, 0, bulletAngles [i]));
			FireBullet(ref bulletindex, transform.position, rotation);
		}
	}

	void FireBullet(ref int bulletindex, Vector3 position, Quaternion rotation) {
		while(bulletindex<MAX_BULLETS) {
			if (!bullets[bulletindex].activeSelf) {
				bullets[bulletindex].transform.position = position;
				bullets[bulletindex].transform.rotation = rotation;
				bullets[bulletindex].SetActive(true);
				return;
			}
			bulletindex++;
		}
		Debug.Log ("Unable to fire bullet");
	}
}

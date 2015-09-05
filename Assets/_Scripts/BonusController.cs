using UnityEngine;
using System.Collections;

public class BonusController : MonoBehaviour {

	public GameObject playerCapsule;
	public GameObject playerShieldPrefab;
	public CapsuleSpawnController capsuleSpawnController;
	
	private BulletSpawnController bulletSpawnController;
	private GameObject shield;
	private Light playerLight;
	private Material playerCapsuleMaterial;
	private Color color;
	private Color[] colors;
	private float bousDeactivationTime;

	private static int[] angles0 = new int[] {0};
	private static int[] angles1 = new int[] {-2, 0, 2};
	private static int[] angles2 = new int[] {-90, 0, 90, 180};
	
	void Start() {
		shield = Instantiate(playerShieldPrefab, transform.position, Quaternion.identity) as GameObject;
		shield.SetActive (false);
		bulletSpawnController = GetComponentInChildren<BulletSpawnController> ();
		playerLight = playerCapsule.GetComponent<Light> ();
		playerCapsuleMaterial = playerCapsule.GetComponent<MeshRenderer> ().material;
		InvokeRepeating ("Spawn", 3f, 10f);	
		colors = new Color[] {
			playerCapsuleMaterial.color,
			Color.red,
			Color.green,
			Color.magenta,
			Color.blue,
		};
	}

	#if UNITY_EDITOR
	void Update() {
		if (Input.GetKeyDown (KeyCode.B)) {
			Spawn();
		}
	}
	#endif

	void Spawn () {
		int bonus = Random.Range (1, colors.Length);
		capsuleSpawnController.Spawn (bonus, colors[bonus]);
	}

	public void SetBonus(int bonus) {
		playerLight.color = colors[bonus];
		playerCapsuleMaterial.color = colors [bonus];	
		switch (bonus) {
		case 0:
#if _DEBUG
			shield.SetActive (true);
			bulletSpawnController.SetBulletCount(5);
			bulletSpawnController.SetBulletAngles(new int[] {-5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5});
#else
			shield.SetActive (false);
			bulletSpawnController.SetBulletCount(1);
			bulletSpawnController.SetBulletAngles(angles0);
#endif
			break;
		case 1:
			bulletSpawnController.SetBulletCount(3);
			bulletSpawnController.SetBulletAngles(angles0);
			break;
		case 2:
			bulletSpawnController.SetBulletCount(2);
			bulletSpawnController.SetBulletAngles(angles1);
			break;
		case 3:
			bulletSpawnController.SetBulletCount(1);
			bulletSpawnController.SetBulletAngles(angles2);
			break;
		case 4:
			shield = Instantiate(playerShieldPrefab, transform.position, Quaternion.identity) as GameObject;
			shield.transform.parent = transform;
			break;
		}
		if (bonus != 0) {
			float bonusDuration = 5f;
			bousDeactivationTime = Time.time + bonusDuration;
			Invoke ("DeactivateBonus", bonusDuration);
		}
	}
	
	void DeactivateBonus() {
		if (Time.time >= bousDeactivationTime) {
			SetBonus (0);
		}
	}
	
}

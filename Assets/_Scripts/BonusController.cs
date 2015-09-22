using UnityEngine;
using System.Collections;

public class BonusController : MonoBehaviour {

	public GameObject playerCapsule;
	public CapsuleSpawnController capsuleSpawnController;

	private PlayerController playerController;
	private BulletSpawnController bulletSpawnController;
	private Material playerGlowMaterial;
	private Material playerCapsuleMaterial;
	private Color color;
	private Color[] colors;
	private float bousDeactivationTime;

	private static int[] angles0 = new int[] {0};
	private static int[] angles1 = new int[] {-2, 0, 2};
	private static int[] angles2 = new int[] {-90, 0, 90, 180};
	
	void Start() {
		bulletSpawnController = GetComponentInChildren<BulletSpawnController> ();
		playerController = GetComponentInChildren<PlayerController> ();
		playerGlowMaterial = playerCapsule.transform.GetChild(0).GetComponent<MeshRenderer>().material;
		playerCapsuleMaterial = playerCapsule.GetComponent<MeshRenderer> ().material;
		InvokeRepeating ("Spawn", 3f, 10f);	
		colors = new Color[] {
			playerCapsuleMaterial.color,
			new Color (1f, 0f, 0f, .7f),
			new Color (0f, .6f, 0f, .6f),
			new Color (.8f, 0f, .8f, .6f),
			new Color (.1f, .1f, .7f, .9f),
		};
		SetBonus (0);
	}

	#if UNITY_EDITOR
	void Update() {
		if (Input.GetKeyDown (KeyCode.B)) {
			Spawn();
		}
		if (Input.GetKeyDown (KeyCode.Alpha0)) {
			Application.LoadLevel(0);
		}
	}
	#endif

	void Spawn () {
		int bonus = Random.Range (1, colors.Length);
		capsuleSpawnController.Spawn (bonus, colors[bonus]);
	}

	public void SetBonus(int bonus) {
		playerCapsuleMaterial.color = colors [bonus];	
		playerGlowMaterial.color = colors[bonus];
		playerGlowMaterial.SetFloat("_Intensity", 1f);
		switch (bonus) {
		case 0:
#if _DEBUG
			playerController.SetShieldActive(true);
			bulletSpawnController.SetBulletCount(5);
			bulletSpawnController.SetBulletAngles(new int[] {-5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5});
#else
			playerController.SetShieldActive(false);

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
			playerController.SetShieldActive(true);
			break;
		}
		if (bonus != 0) {
			float bonusDuration = 6f;
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

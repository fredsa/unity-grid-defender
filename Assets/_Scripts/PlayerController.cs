using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerBounds {
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;
}

public class PlayerController : MonoBehaviour {
	
	public GameObject playerExplosionPrefab;
	public GameObject enemyExplosionPrefab;
	public GameObject playerShieldPrefab;
	public PlayerBounds bounds;
	public Transform grid;
	public bool invinsible = false;

	private GameObject playerCapsule;
	private Material playerCapsuleMaterial;
	private Light playerLight;
	private float fingerYOffset = 2f;
	private float maxTrackSpeed = 40f;
	private float keyboardSpeedMultiplier = .4f;
	private Plane playerPlane;
	private GameObject shield;
#if _DEBUG
#else
	private Animator animator;
	private int PlayerDeathProperty = Animator.StringToHash ("PlayerDeath");
#endif
	private Color bonusColor;
	private Color startingColor;
	private BulletSpawnController bulletSpawnController;

	public void SetBonusColor(Color bonusColor) {
		this.bonusColor = bonusColor;
#if _DEBUG
		bulletSpawnController.SetBulletCount(5);
		bulletSpawnController.SetBulletAngles(new int[] {-5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5});
#else
		switch (Random.Range (0, 4)) {
		case 0:
			bulletSpawnController.SetBulletCount(3);
			bulletSpawnController.SetBulletAngles(new int[] {0});
			break;
		case 1:
			bulletSpawnController.SetBulletCount(2);
			bulletSpawnController.SetBulletAngles(new int[] {-2, 0, 2});
			break;
		case 2:
			bulletSpawnController.SetBulletCount(1);
			bulletSpawnController.SetBulletAngles(new int[] {-90, 0, 90, 180});
			break;
		case 3:
			shield = Instantiate(playerShieldPrefab, transform.position, Quaternion.identity) as GameObject;
			shield.transform.parent = transform;
			Invoke ("DestroyShield", 5f);
			break;
		}
#endif
	}

	void DestroyShield() {
		Destroy (shield);
	}

	void Start() {
		bulletSpawnController = GetComponentInChildren<BulletSpawnController> ();
		playerCapsule = transform.GetChild (0).gameObject;
		playerCapsuleMaterial = playerCapsule.GetComponent<MeshRenderer> ().material;
		playerLight = playerCapsule.GetComponent<Light> ();
		startingColor = playerCapsuleMaterial.color;
		bonusColor = startingColor;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		playerPlane = new Plane(Vector3.forward, transform.position);
#if _DEBUG
		transform.position = new Vector3(bounds.xMin - 3f, bounds.yMax - 3f, transform.position.z);
		SetBonusColor(new Color(.96f, 0f, 1f, .4f));
#else
		animator = gameObject.GetComponent<Animator> ();
#endif
	}

	void Update() {
		playerLight.color = bonusColor;
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

#if _DEBUG
#else
		targetPosition = Clamp (targetPosition, bounds);
#endif
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, maxTrackSpeed * Time.deltaTime);

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Enemy") || other.gameObject.CompareTag ("Enemy Obstacle")) {
			Destroy(other.gameObject);
			if (!invinsible) {
				invinsible = true;
				Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
				Instantiate(playerExplosionPrefab, transform.position, Quaternion.identity);
				bonusColor = startingColor;
				FindObjectOfType<GameController>().SubtractLife();
#if _DEBUG
#else
				animator.SetTrigger (PlayerDeathProperty);
#endif
			}
		}
	}

	private Vector3 Clamp (Vector3 pos, PlayerBounds bounds) {
		pos.x = Mathf.Clamp (pos.x, bounds.xMin, bounds.xMax);
		pos.y = Mathf.Clamp (pos.y, bounds.yMin, bounds.yMax);
		return pos;
	}
}

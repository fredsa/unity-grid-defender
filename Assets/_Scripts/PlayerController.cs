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
	
	public GameObject playerCapsule;
	public GameObject playerExplosionPrefab;
	public GameObject gameOverText;
	public GameObject startButton;
	public PlayerBounds bounds;
	public Transform grid;
	public bool invinsible = false;

	private GameController gameController;
	private BonusController bonusController;
	private float fingerYOffset = 2f;
	private float maxTrackSpeed = 40f;
	private float keyboardSpeedMultiplier = .4f;
	private Plane playerPlane;
#if _DEBUG
#else
	private Animator animator;
	private int PlayerDeathProperty = Animator.StringToHash ("Player Death");
	private int gameOverProperty = Animator.StringToHash ("Game Over");
#endif

	public void SetBonus(int bonus) {
		bonusController.SetBonus (bonus);
	}

	void Start() {
		bonusController = FindObjectOfType<BonusController> ();
		gameController = FindObjectOfType<GameController> ();
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		playerPlane = new Plane(Vector3.forward, transform.position);
#if _DEBUG
		transform.position = new Vector3(bounds.xMin - 3f, bounds.yMax - 3f, transform.position.z);
		SetBonusColor(new Color(.96f, 0f, 1f, .4f));
#else
		animator = gameObject.GetComponent<Animator> ();
#endif
	}

	public void SetGameOver(bool gameOver) {
		if (gameOver) {
			animator.SetBool (gameOverProperty, gameOver);
			gameOverText.SetActive (gameOver);
			startButton.SetActive(gameOver);
		} else {
			Application.LoadLevel(0);
		}
	}
	
	void Update() {
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
		Debug.Assert(other.CompareTag ("Enemy") || other.CompareTag ("Enemy Obstacle") || other.CompareTag ("Bonus"), other.gameObject.name);
		if (other.CompareTag ("Bonus")) {
			return;
		}
		Destroy(other.gameObject);
		if (!invinsible) {
			invinsible = true;
			Instantiate(playerExplosionPrefab, transform.position, Quaternion.identity);
			bonusController.SetBonus(0);
#if _DEBUG
#else
			if (gameController.SubtractLife() == 0) {
				SetGameOver(true);
			} else {
				animator.SetTrigger (PlayerDeathProperty);
			}
#endif
		}
	}

	private Vector3 Clamp (Vector3 pos, PlayerBounds bounds) {
		pos.x = Mathf.Clamp (pos.x, bounds.xMin, bounds.xMax);
		pos.y = Mathf.Clamp (pos.y, bounds.yMin, bounds.yMax);
		return pos;
	}
}

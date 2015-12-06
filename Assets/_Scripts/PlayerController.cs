using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerBounds
{
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;
}

public class PlayerController : MonoBehaviour
{
	
	public GameObject playerCapsule;
	public GameObject playerExplosionPrefab;
	public GameObject playerShieldPrefab;
	public GameObject gameOverText;
	public GameObject startButton;
	public AudioSource gameOverAudioSource;
	public PlayerBounds bounds;
	public Transform grid;

	// controlled by animator
	[HideInInspector]
	public bool
		invinsible;

	private BonusController bonusController;
	private GameObject playerShield;
	private float fingerYOffset = 2f;
	private float maxTrackSpeed = 40f;
	private float keyboardSpeedScale = 20f;
	private Plane playerPlane;
	private Animator animator;
	private int gameOverProperty = Animator.StringToHash ("Game Over");
#if _DEBUG
#else
	private GameController gameController;
	private int playerDeathProperty = Animator.StringToHash ("Player Death");
#endif

	public void SetBonus (int bonus)
	{
		bonusController.SetBonus (bonus);
	}

	void Start ()
	{
		playerShield = Instantiate (playerShieldPrefab);
		playerShield.transform.parent = transform;
		bonusController = FindObjectOfType<BonusController> ();
#if _DEBUG
#else
		gameController = FindObjectOfType<GameController> ();
#endif
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		playerPlane = new Plane (Vector3.forward, transform.position);
#if _DEBUG
		transform.position = new Vector3(bounds.xMin - 3f, bounds.yMax - 3f, transform.position.z);
#endif
		animator = gameObject.GetComponent<Animator> ();
	}

	public void SetGameOver (bool gameOver)
	{
		if (gameOver) {
			animator.SetBool (gameOverProperty, gameOver);
			gameOverText.SetActive (gameOver);
			startButton.SetActive (gameOver);
			gameOverAudioSource.Play ();
		} else {
			Application.LoadLevel (0);
		}
	}

	public void SetShieldActive (bool active)
	{
		playerShield.SetActive (active);
	}

	void Update ()
	{
		Vector3 targetPosition;
		if (Input.GetMouseButton (0)) {
			Vector3 pos = Input.mousePosition;
			Ray ray = Camera.main.ScreenPointToRay (pos);
			float distance;
			playerPlane.Raycast (ray, out distance);
			targetPosition = ray.GetPoint (distance);
			targetPosition.y += fingerYOffset;
		} else {
			float scale = keyboardSpeedScale * Time.deltaTime;
			targetPosition = transform.position + new Vector3 (Input.GetAxisRaw ("Horizontal") * scale, Input.GetAxisRaw ("Vertical") * scale, 0f);
		}

#if _DEBUG
#else
		targetPosition = Clamp (targetPosition, bounds);
#endif
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, maxTrackSpeed * Time.deltaTime);

	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Assert (other.CompareTag ("Enemy") || other.CompareTag ("Enemy Obstacle") || other.CompareTag ("Bonus"), other.gameObject.name);
		if (other.CompareTag ("Bonus")) {
			return;
		}
		Destroy (other.gameObject);
		if (!invinsible && !playerShield.activeSelf) {
			Instantiate (playerExplosionPrefab, transform.position, Quaternion.identity);
			bonusController.SetBonus (0);
#if _DEBUG
#else
			if (gameController.SubtractLife () == 0) {
				SetGameOver (true);
			} else {
				animator.SetTrigger (playerDeathProperty);
			}
#endif
		}
	}

	private Vector3 Clamp (Vector3 pos, PlayerBounds bounds)
	{
		pos.x = Mathf.Clamp (pos.x, bounds.xMin, bounds.xMax);
		pos.y = Mathf.Clamp (pos.y, bounds.yMin, bounds.yMax);
		return pos;
	}
}

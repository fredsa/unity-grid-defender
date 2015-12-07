using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GameController : MonoBehaviour
{

	public CanvasTextController scoreTextController;
	public CanvasTextController livesTextController;
	public CanvasTextController highScoreTextController;
	public PlayerController playerController;
	public GameObject gameOverPanel;
	public GameObject gameOverText;
	public Button startButton;
	public AudioSource gameOverAudioSource;

	bool startPressed;
	int lives;
	int score;
	bool playing;

	int gameOverProperty = Animator.StringToHash ("Game Over");

	public void StartPressed ()
	{
		startPressed = true;
	}

    #if !UNITY_EDITOR
    void Awake()
    {
        Debug.Log("Application.version: " + Application.version);
        Debug.Log("Application.installMode: " + Application.installMode);
        Debug.Log("Application.genuineCheckAvailable: " + Application.genuineCheckAvailable);
        Debug.Log("Application.genuine: " + Application.genuine);
    }
    #endif

	void Start ()
	{
		scoreTextController.SetValue (score);
#if UNITY_EDITOR
		lives = 2;
#else
    	lives = 3;
#endif
		livesTextController.SetValue (lives);
		StartCoroutine (GameLoop ());
	}

	IEnumerator GameLoop ()
	{
		playing = true;
		gameOverPanel.SetActive (false);
		while (playing) {
			yield return null;
		}
		yield return StartCoroutine (GameOver ());
		yield return StartCoroutine (Restart ());
		yield return null;
	}

	void ReportFinalScore (int score)
	{
		int high_score = PlayerPrefs.GetInt ("high_score", 0);
		if (score > high_score) {
			high_score = score;
			PlayerPrefs.SetInt ("high_score", high_score);
		}
		highScoreTextController.SetValue (high_score);
	}

	IEnumerator GameOver ()
	{
		startPressed = false;
		playerController.animator.SetBool (gameOverProperty, true);
		gameOverPanel.SetActive (true);
		gameOverAudioSource.Play ();
		ReportFinalScore (score);
		while (!startPressed) {
			yield return null;
		}
	}

	IEnumerator Restart ()
	{
		Application.LoadLevel (0);
		yield return null;
	}

	public void AddPoints (int points)
	{
		score += points;
		scoreTextController.SetValue (score);
	}

	public int SubtractLife ()
	{
		lives--;
		#if _DEBUG
		#else
		if (lives <= 0) {
			playing = false;
		}
		#endif

		livesTextController.SetValue (lives);
		return lives;
	}

}

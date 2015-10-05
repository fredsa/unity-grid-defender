using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

	public CanvasTextController scoreTextController;
	public CanvasTextController livesTextController;

#if !UNITY_EDITOR
	void Awake ()
	{
		Debug.Log ("Application.version: " + Application.version);
		Debug.Log ("Application.installMode: " + Application.installMode);
		Debug.Log ("Application.genuineCheckAvailable: " + Application.genuineCheckAvailable);
		Debug.Log ("Application.genuine: " + Application.genuine);
	}
#endif

	void Start ()
	{
		scoreTextController.SetValue (0);
#if UNITY_EDITOR
		livesTextController.SetValue (2);
#else
		livesTextController.SetValue (3);
#endif
	}

	public void AddPoints (int points)
	{
		scoreTextController.IncrementValue (points);
	}

	public int SubtractLife ()
	{
		return livesTextController.IncrementValue (-1);
	}

}

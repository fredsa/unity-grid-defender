using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasTextController : MonoBehaviour
{

	public string format = "{0,-4:n0}";

	int value;
	Text valueText;
	Animator animator;

	static CanvasTextController instance;

	void Awake ()
	{
		valueText = gameObject.GetComponent<Text> ();
		animator = gameObject.GetComponent<Animator> ();
		UpdateScore ();
	}

	public void SetValue (int value)
	{
		this.value = value;
		UpdateScore ();
	}

	private void UpdateScore ()
	{
		valueText.text = string.Format (format, value);
		animator.SetTrigger ("ValueChanged");
	}

}

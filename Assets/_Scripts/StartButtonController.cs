using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartButtonController : MonoBehaviour
{

	void Update ()
	{
		// Android TV remote and DPAD support
		if (Input.GetButtonUp ("Submit")) {
			gameObject.GetComponent<Button> ().onClick.Invoke ();
		}
	}
}

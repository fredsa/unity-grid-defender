using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FpsController : MonoBehaviour
{
	static string FPS_FORMAT = "{0:0.} fps";
	
	static float updateInterval = 0.25f;
	
	private Text text;
	private float accum;
	private int frames;
	private float timeleft = updateInterval;
	private Color color = new Color (1f, 0f, 0f, 1f);

#if _DEBUG
#else
	void Awake ()
	{
		Destroy (gameObject);
	}
#endif

	void Start ()
	{
		text = gameObject.GetComponent<Text> ();
	}
	
	void Update ()
	{
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		frames++;

		if (timeleft <= 0f) {
			float fps = accum / frames;
			text.text = string.Format (FPS_FORMAT, fps);
			color.r = (60f - fps) / 60f;
			text.color = color;
			timeleft = updateInterval;
			accum = 0f;
			frames = 0;
		}
	}
}

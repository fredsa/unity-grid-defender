using UnityEngine;
using UnityEngine.UI;
	
[RequireComponent(typeof(Text))]
public class FpsCounterController : MonoBehaviour
{
	private static float BAD_FPS = 30f;
	private static float GOOD_FPS = 60f;
		
	private Text text;
	private float fps = 60;
	private Color color = new Color (0f, 0f, 0f);
		
	void Awake ()
	{
#if _DEBUG || UNITY_EDITOR || DEVELOPMENT_BUILD  
		text = GetComponent<Text> ();
#else
		Destroy (gameObject);
#endif		
	}
		
	void Update ()
	{
		float interp = Time.deltaTime / (0.5f + Time.deltaTime);
		float currentFPS = 1.0f / Time.deltaTime;
		fps = Mathf.Lerp (fps, currentFPS, interp);
		text.text = string.Format ("{0:0} fps", fps);
			
		// adjust red/green color
		float fps_clamped = Mathf.Clamp (fps, BAD_FPS, GOOD_FPS);
		float green_red_ratio = (fps_clamped - BAD_FPS) / (GOOD_FPS - BAD_FPS);
		color.g = green_red_ratio;
		color.r = 1f - green_red_ratio;
		text.color = color;
	}
}

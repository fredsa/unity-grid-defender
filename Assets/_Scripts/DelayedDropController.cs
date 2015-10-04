using UnityEngine;
using System.Collections;

public class DelayedDropController : MonoBehaviour
{

	public float delay = 0f;
	public float speed = 10f;
	bool isDropping = false;

	void Start ()
	{
		Invoke ("Drop", delay);
	}

	void Drop ()
	{
		isDropping = true;
	}

	void FixedUpdate ()
	{
		if (isDropping) {
			transform.position += new Vector3 (0f, -speed * Time.deltaTime, 0f);
		}
	}
}

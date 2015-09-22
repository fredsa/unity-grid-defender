using UnityEngine;
using System.Collections;

public class TumbleController : MonoBehaviour
{

	void Start ()
	{
		GetComponent<Rigidbody> ().angularVelocity = Random.rotation.eulerAngles;
	}
}

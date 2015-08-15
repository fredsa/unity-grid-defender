using UnityEngine;
using System.Collections;

public class CapsuleSpawnController : MonoBehaviour {

	public GameObject capsulePrefab;

	private GameObject playbox;
	private GameObject capsule;

	void Start () {
		playbox = GameObject.FindWithTag ("Playbox");
		InvokeRepeating ("Spawn", 3f, 10f);	
	}
	
	void Spawn () {
		Vector3 position = new Vector3 (
			Random.Range (-playbox.transform.localScale.x/2, playbox.transform.localScale.x/2),
			playbox.transform.localScale.y/2 + transform.localScale.y * 2,
			0);
		Color color = new Color (
			Random.Range (.2f, .6f),
			Random.Range (.2f, .6f),
			Random.Range (.2f, .6f),
			1f);
		capsule = Instantiate (capsulePrefab, position, Quaternion.identity) as GameObject;
		capsule.GetComponent<MeshRenderer> ().material.color = color;

		color.a = .5f;
		capsule.GetComponent<Light>().color = color;
		capsule.transform.parent = transform;
	}
}

using UnityEngine;
using System.Collections;

public class CapsuleSpawnController : MonoBehaviour
{

	public GameObject capsulePrefab;

	private GameObject playbox;
	private GameObject capsule;

	void Start ()
	{
		playbox = GameObject.FindWithTag ("Playbox");
	}

	public void Spawn (int bonusId, Color color)
	{
		Vector3 position = new Vector3 (
			Random.Range (-playbox.transform.localScale.x / 2, playbox.transform.localScale.x / 2),
			playbox.transform.localScale.y / 2 + transform.localScale.y * 2,
			0);
		capsule = Instantiate (capsulePrefab, position, Quaternion.identity) as GameObject;
		capsule.GetComponentInChildren<MeshRenderer> ().material.color = color;
		capsule.GetComponentInChildren<CapsuleController> ().bonusId = bonusId;
		capsule.transform.GetChild (1).GetComponent<MeshRenderer> ().material.color = color;
		capsule.transform.parent = transform;
	}
}

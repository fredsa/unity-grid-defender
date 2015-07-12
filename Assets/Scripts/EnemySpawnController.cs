using UnityEngine;
using System.Collections;

public class EnemySpawnController : MonoBehaviour {

	public GameObject enemyPrefab;
	public float speed = 1f;
	public float delay = 0f;
	public float rate = 1f;
	
	LayerMask layermask;
	bool needSpawn = false;
	float checkRadius;
	
	void Start () {
		layermask = LayerMask.GetMask("Enemies");
		InvokeRepeating ("Spawn", delay, rate);
	}
	
	void Spawn () {
		needSpawn = true;
	}
	
	void Update() {
		if (!needSpawn) {
			return;
		}
		
		needSpawn = false;
		GameObject enemyClone = Instantiate (enemyPrefab, transform.position, transform.rotation) as GameObject;
		enemyClone.transform.parent = gameObject.transform;
	}
}

using UnityEngine;
using System.Collections;

public class EnemySpawnController : MonoBehaviour {

	public GameObject enemyPrefab;
	public float speed = 1f;
	public float delay = 0f;
	public float rate = 1f;
	public Color enemyColor;
	public int chainLength = 5;
	float tumble = 10f;

	int chainCount;
	bool needSpawn = false;
	float checkRadius;
	
	void Start () {
		InvokeRepeating ("Spawn", delay, rate);
	}
	
	void Spawn () {
		needSpawn = true;
	}
	
	void Update() {
		if (!needSpawn) {
			return;
		}

		GameObject target = null;
		Vector3 angularVelocity = new Vector3 (0f, 0f, tumble + tumble * Random.value);
		chainCount++;
		for (int i=0; i<chainLength; i++) {
			needSpawn = false;
			GameObject enemyClone = Instantiate (enemyPrefab, transform.position, transform.rotation) as GameObject;
			enemyClone.name += chainCount + "-" + i;
			enemyClone.transform.parent = gameObject.transform;
//			enemyClone.gameObject.GetComponent<Rigidbody> ().angularVelocity = angularVelocity;
			enemyClone.gameObject.GetComponent<MeshRenderer> ().material.color = enemyColor * i/chainLength;

			ChainController ctrl = enemyClone.GetComponent<ChainController> ();
			ctrl.Setup(target, transform.right * speed);

			target = enemyClone;
		}
	}
}

using UnityEngine;
using System.Collections;

public class EnemySpawnController : MonoBehaviour {

	public GameObject enemyPrefab;
	public float enemySpeed = 7f;
	public float timeToFirstSpawn = 0f;
	public float spawnRate = 4f;
	public Color chainColor;
	int chainLength = 15;

	int chainCount;
	float checkRadius;
	
	void Start () {
		InvokeRepeating ("Spawn", timeToFirstSpawn, spawnRate);
	}
	
	void Spawn () {
		GameObject target = null;
		chainCount++;
		for (int i=0; i<chainLength; i++) {
			GameObject enemyClone = Instantiate (enemyPrefab, transform.position, transform.rotation) as GameObject;
			enemyClone.name += chainCount + "-" + i;
			enemyClone.transform.parent = gameObject.transform;
			enemyClone.gameObject.GetComponent<MeshRenderer> ().material.color = chainColor * i/chainLength;

			ChainController chainController = enemyClone.GetComponent<ChainController> ();
			chainController.Setup(target, transform.right * enemySpeed);
			chainController.SetIndex(i);

			target = enemyClone;
		}
	}
}

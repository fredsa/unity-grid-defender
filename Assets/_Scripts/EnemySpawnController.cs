using UnityEngine;
using System.Collections;

public class EnemySpawnController : MonoBehaviour {

	public GameObject enemyPrefab;
	public GameObject playbox;
	public float enemySpeed = 7f;
	public float timeToFirstSpawn = 0f;
	public float spawnRate = 4f;
	public Color chainColor;

	private int chainLength = 15;
	private float checkRadius;
	private GameObject[] enemies;
	
	void Start () {
		enemies = new GameObject[chainLength];
		InvokeRepeating ("Spawn", timeToFirstSpawn, spawnRate);
	}
	
	void Spawn () {
		for (int i=0; i<chainLength; i++) {
			if (enemies[i]) {
				return;
			}
		}
		GameObject target = null;
		for (int i=0; i<chainLength; i++) {
			enemies[i] = Instantiate (enemyPrefab, transform.position, transform.rotation) as GameObject;
			enemies[i].name += i;
			enemies[i].transform.parent = gameObject.transform;
			Color color = MakeColor (i);
			enemies [i].gameObject.GetComponent<MeshRenderer> ().material.color = color;
			enemies[i].gameObject.GetComponent<Light> ().color = color;

			ChainController chainController = enemies[i].GetComponent<ChainController> ();
			chainController.Setup(playbox, target, transform.right * enemySpeed);

			target = enemies[i];
		}
	}

	Color MakeColor (int i) {
		float factor = (float)i/chainLength * .75f + .25f;
		return chainColor * factor;
	}
}

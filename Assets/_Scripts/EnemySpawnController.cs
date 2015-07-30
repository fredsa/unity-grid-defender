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
			enemies[i].gameObject.GetComponent<MeshRenderer> ().material.color = MakeColor(i);

			ChainController chainController = enemies[i].GetComponent<ChainController> ();
			chainController.Setup(playbox, target, transform.right * enemySpeed);
			chainController.SetIndex(i);

			target = enemies[i];
		}
	}

	Color MakeColor (int i) {
		return chainColor;
		float factor = i/chainLength * .5f + .5f;
		return new Color(chainColor.r * factor, chainColor.g * factor, chainColor.b * factor);
	}
}

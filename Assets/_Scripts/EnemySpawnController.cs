using UnityEngine;
using System.Collections;

public class EnemySpawnController : MonoBehaviour {

	public GameObject enemyPrefab;
	public GameObject playbox;
	public float enemySpeed = 7f;
	public float timeToFirstSpawn = 0f;
	public float spawnRate = 4f;
	public Color chainColor;
	public int chainLength = 15;

	private float checkRadius;
	
	void Start () {
#if _DEBUG
		timeToFirstSpawn = 0f;
		spawnRate = 1f;
		chainLength *= 2;
#endif
		InvokeRepeating ("Spawn", timeToFirstSpawn, spawnRate);
	}
	
	void Spawn () {
		if (transform.childCount > 0) {
			return;
		}
		GameObject previousEnemy = null;
		for (int i=0; i<chainLength; i++) {
			GameObject enemy = Instantiate (enemyPrefab, transform.position, transform.rotation) as GameObject;
			enemy.name += i;
			enemy.transform.parent = gameObject.transform;
			Color color = MakeColor (i);
			enemy.GetComponent<MeshRenderer> ().material.color = color;
			enemy.GetComponent<Light> ().color = color;
#if _DEBUG
			enemy.GetComponent<Light> ().enabled = GameController.DEBUG_ENEMY_LIGHTS_ON;
#endif

			ChainController chainController = enemy.GetComponent<ChainController> ();
			chainController.Setup(playbox, previousEnemy, transform.right * enemySpeed);

			previousEnemy = enemy;
		}
	}

	Color MakeColor (int i) {
		float factor = (float)i/chainLength * .8f + .2f;
		return chainColor * factor;
	}
}

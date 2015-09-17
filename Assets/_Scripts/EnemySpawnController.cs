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
			GameObject disc = enemy.transform.GetChild(0).gameObject;
			GameObject glow = enemy.transform.GetChild(1).gameObject;
			disc.GetComponent<MeshRenderer> ().material.color = color;
			glow.GetComponent<MeshRenderer> ().material.color = color;

			ChainController chainController = enemy.GetComponent<ChainController> ();
			chainController.Setup(playbox, previousEnemy, transform.right * enemySpeed);

			previousEnemy = enemy;
		}
	}

	Color MakeColor (int i) {
		float factor = (float)(i + 1) / chainLength * .75f + .25f;
		return new Color (chainColor.r * factor, chainColor.g * factor, chainColor.b * factor, chainColor.a);
	}
}

using UnityEngine;
using System.Collections;

public class GridTextureController : MonoBehaviour {

	private float speed = 0.8f;

	private Material material;
	private Vector2 offset = new Vector2();
	private float textureScaleX;
	private float textureScaleY;
//	private Transform playerTransform;

	void Start () {
//		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		material = GetComponent<MeshRenderer> ().material;
		textureScaleX = material.mainTextureScale.x;
		textureScaleY = material.mainTextureScale.y;
	}
	
	void Update () {
//		offset.x = playerTransform.position.x / textureScaleX;
//		offset.y = playerTransform.position.y / textureScaleY + Time.time * speed;
		offset.y = Time.time * speed;
		material.mainTextureOffset = offset;
	}
}

using UnityEngine;
using System.Collections;

public class DetachPlaySelfDestructController : MonoBehaviour
{

	public void DetachPlaySelfDestruct ()
	{
		AudioSource source = GetComponent<AudioSource> ();
		source.Play ();
		transform.parent = null;
		Destroy (gameObject, source.clip.length);
	}
	
}

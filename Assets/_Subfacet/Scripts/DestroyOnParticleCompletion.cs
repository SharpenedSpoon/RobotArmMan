using UnityEngine;
using System.Collections;

public class DestroyOnParticleCompletion : MonoBehaviour {

	private ParticleSystem particleSystem = null;

	// Use this for initialization
	void Start () {
		particleSystem = GetComponent<ParticleSystem>();
		if (particleSystem == null) {
			enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (particleSystem.isStopped) {
			Destroy(gameObject);
		}
	}
}

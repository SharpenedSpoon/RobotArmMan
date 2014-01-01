using UnityEngine;
using System.Collections;


public class CanFallToDeath : MonoBehaviour {

	public float minY = -100;

	private Health health;
	private Transform tr;

	void Start() {
		health = GetComponent<Health>();
		if (health == null) {
			Debug.LogError("CanFallToDeath requires the Health component. Deactivating.");
			enabled = false;
		}
		tr = transform; // cache this for supposedly faster lookups
	}
	
	// Update is called once per frame
	void Update () {
		if (tr.position.y <= minY) {
			health.Die();
		}
	}
}

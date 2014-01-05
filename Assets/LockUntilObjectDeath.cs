using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LockUntilObjectDeath : MonoBehaviour {

	public List<GameObject> lockingObjects;

	void Update () {

		// ----- Remove dead objects
		lockingObjects.RemoveAll(item => item == null);

		// ----- Open if all objects are dead
		if (lockingObjects.Count == 0) {
			Destroy(gameObject);
		}
	}
}

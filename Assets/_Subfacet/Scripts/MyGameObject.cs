using UnityEngine;
using System.Collections;

public class MyGameObject : MonoBehaviour {

	
	void Start () {
	
	}

	void Update () {
	
	}

	public virtual void Die() {
		Destroy(gameObject);
	}


}

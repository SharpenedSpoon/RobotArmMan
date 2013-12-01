using UnityEngine;
using System.Collections;

public class MyGameObject : MonoBehaviour {

	public float Speed = 10.0f;
	[HideInInspector]
	public bool IsFacingRight = true;
	[HideInInspector]
	public int Direction = 1;
	
	void Start () {
	
	}

	void Update () {
	
	}

	public virtual void Die() {
		Destroy(gameObject);
	}

	public void Flip() {
		IsFacingRight = IsFacingRight ? false : true;
		Direction = IsFacingRight ? 1 : -1;
		transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
	
	public void FaceLeft() {
		if (IsFacingRight) {
			Flip();
		}
	}
	
	public void FaceRight() {
		if (!IsFacingRight) {
			Flip();
		}
	}
}

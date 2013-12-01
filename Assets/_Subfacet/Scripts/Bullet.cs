using UnityEngine;
using System.Collections;

public class Bullet : MyGameObject {

	public int Damage = 1;
	public float Lifetime = 20.0f;
	public bool DestroyOnHit = true;

	[HideInInspector]
	public string OwnerTag = "Player";
	[HideInInspector]
	private int _horizontalDirection = 1;
	private float _lifeTimer = 0.0f;
	
	void Start () {
		GameObject player = GameObject.Find("Player");
		if (player != null) {
			IsFacingRight = player.GetComponent<MyGameObject>().IsFacingRight;
		}
		if (IsFacingRight) {
			_horizontalDirection = 1;
		} else {
			_horizontalDirection = -1;
		}

		rigidbody2D.velocity = transform.right * Speed * _horizontalDirection;
	}

	void Update () {
		_lifeTimer += Time.deltaTime;
		if (_lifeTimer >= Lifetime) {
			Die();
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (!col.gameObject.CompareTag(OwnerTag)) {
			if (col.gameObject != null) {
				Debug.Log (col.gameObject.name);
				col.gameObject.SendMessage("TakeDamage", 1);
			}
			if (DestroyOnHit) {
				Die();
			}
		}
	}
}

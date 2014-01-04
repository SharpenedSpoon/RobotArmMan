using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int Damage = 1;
	public float Lifetime = 20.0f;
	public bool DestroyOnHit = true;
	public float Speed = 20.0f;
	public bool stunOnHit = false;
	public float stunTimeMilliseconds = 1000.0f;

	[HideInInspector]
	public string OwnerTag = "Player";
	[HideInInspector]
	public GameObject OwnerObject = null;
	[HideInInspector]
	public int Direction = 0;
	[HideInInspector]
	public bool FacingRight;

	private float _lifeTimer = 0.0f;
	private Health _health = null;

	void Start () {
		_health = GetComponent<Health>();
	}

	void Update () {
		_lifeTimer += Time.deltaTime;
		if (_lifeTimer >= Lifetime) {
			_health.Die();
		}
	}

	public void ShootInDirection(int dir) {
		rigidbody2D.velocity = transform.right * Speed * dir;
	}

	public void SetFacingRight(bool facingRightInput) {
		FacingRight = facingRightInput;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject != OwnerObject) {
			//if (!col.gameObject.CompareTag(OwnerTag)) {
				if (col.gameObject != null) {
					var healthScript = col.gameObject.GetComponent<Health>();
					if (healthScript != null) {
						healthScript.TakeDamage(1);
					}
					if (stunOnHit) {
						col.gameObject.SendMessage("Stun", stunTimeMilliseconds, SendMessageOptions.DontRequireReceiver);
					}
				}
			//}

			if (DestroyOnHit) {
				_health.Die();
			}
		}
	}
}

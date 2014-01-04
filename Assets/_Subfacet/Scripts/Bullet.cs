using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int damage = 1;
	public float lifetime = 20.0f;
	public bool destroyOnHit = true;
	public float speed = 20.0f;
	public bool stunOnHit = false;
	public float stunTimeMilliseconds = 1000.0f;

	[HideInInspector]
	public string ownerTag = "Player";
	[HideInInspector]
	public GameObject ownerObject = null;
	[HideInInspector]
	public int direction = 0;
	[HideInInspector]
	public bool facingRight;

	private float lifeTimer = 0.0f;
	private Health health = null;

	void Start () {
		health = GetComponent<Health>();
	}

	void Update () {
		lifeTimer += Time.deltaTime;
		if (lifeTimer >= lifetime) {
			health.Die();
		}
	}

	public void ShootInDirection(int dir) {
		rigidbody2D.velocity = transform.right * speed * dir;
	}

	public void SetFacingRight(bool facingRightInput) {
		facingRight = facingRightInput;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject != ownerObject) {
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

			if (destroyOnHit) {
				health.Die();
			}
		}
	}
}

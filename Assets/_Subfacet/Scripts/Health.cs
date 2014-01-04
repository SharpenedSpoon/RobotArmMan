using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int maxHealth = 10;
	public int HP { get; private set; }

	public ParticleSystem deathEffect = null;

	void Start() {
		HP = maxHealth;
	}

	void Update() {
		if (HP <= 0) {
			Die();
		}
	}

	public void TakeDamage(int dmg) {
		HP -= dmg;
	}

	public void GainHealth(int hlh) {
		HP = Mathf.Min(HP + hlh, maxHealth);
	}

	public void Die() {
		//Debug.Log(gameObject.name + " is dying.");
		if (deathEffect != null) {
			Instantiate(deathEffect, transform.position, Quaternion.identity);
		}
		Destroy(gameObject);
	}
}

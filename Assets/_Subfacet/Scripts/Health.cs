﻿using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int MaxHealth = 10;
	public int HP { get; private set; }

	void Start() {
		HP = MaxHealth;
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
		HP = Mathf.Min(HP + hlh, MaxHealth);
	}

	public void Die() {
		Debug.Log(gameObject.name + " is dying.");
		Destroy(gameObject);
	}
}

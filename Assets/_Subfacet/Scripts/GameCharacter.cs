using UnityEngine;
using System.Collections;
using Vectrosity;

public class GameCharacter : MyGameObject {

	public int health = 10;

	public virtual void Update() {
		if (health <= 0) {
			Die();
		}
	}

	public void TakeDamage(int dmg) {
		health -= dmg;
	}
}

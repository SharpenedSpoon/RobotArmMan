using UnityEngine;
using System.Collections;
using Vectrosity;

public class GameCharacter : MyGameObject {

	public int Health = 10;




	
	public virtual void Update() {


		if (Health <= 0) {
			Die();
		}


	}





	public void TakeDamage(int dmg) {
		Health -= dmg;
	}
}

using UnityEngine;
using System.Collections;

public class PlayerCharacter : GameCharacter {

	void Start () {
	
	}

	public override void Update () {
		base.Update();

		// ----- Get the input for movement
		int hor = 0;
		int ver = 0;
		if (Input.GetKey(KeyCode.LeftArrow))  { hor -= 1; }
		if (Input.GetKey(KeyCode.RightArrow)) { hor += 1; }
		if (Input.GetKey(KeyCode.DownArrow))  { ver -= 1; }
		if (Input.GetKey(KeyCode.UpArrow))    { ver += 1; }

		// ----- Apply movement
		Move(hor);
		transform.position += ver * Speed * Time.deltaTime * transform.up;

		// ----- Shoot stuff
		if (Input.GetKey(KeyCode.Z)) {
			Shoot();
		}
	}
}

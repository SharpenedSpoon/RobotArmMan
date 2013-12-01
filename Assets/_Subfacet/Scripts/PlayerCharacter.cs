using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour {

	private Movement _movement = null;
	private Health _health = null;
	private Shooter _shooter = null;

	void Start () {
		_movement = GetComponent<Movement>();
		_health = GetComponent<Health>();
		_shooter = GetComponent<Shooter>();
		if (_movement == null || _health == null || _shooter == null) {
			Debug.Log("WTF bro? Add all components first. - Hugs and Kisses, " + gameObject.name);
			Destroy(gameObject);
		}
	}

	void Update () {
		// ----- Get the input for movement
		int hor = 0;
		int ver = 0;
		if (Input.GetKey(KeyCode.LeftArrow))  { hor -= 1; }
		if (Input.GetKey(KeyCode.RightArrow)) { hor += 1; }
		if (Input.GetKey(KeyCode.DownArrow))  { ver -= 1; }
		if (Input.GetKey(KeyCode.UpArrow))    { ver += 1; }

		// ----- Apply movement
		_movement.Move(hor);
		transform.position += ver * _movement.Speed * Time.deltaTime * transform.up; // vertical movement for debugging

		// ----- Shoot stuff
		if (Input.GetKey(KeyCode.Z)) {
			_shooter.Shoot(_movement.Direction);
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCharacter : MonoBehaviour {

	private Movement _movement = null;
	private Health _health = null;
	private Shooter _shooter = null;

	private Animator _anim;
	public Dictionary<string, bool> AnimationBools = new Dictionary<string, bool>();


	void Start () {
		_movement = GetComponent<Movement>();
		_health = GetComponent<Health>();
		_shooter = GetComponent<Shooter>();
		_anim = GetComponent<Animator>();
		if (_movement == null || _health == null || _shooter == null || _anim == null) {
			Debug.Log("WTF bro? Add all components first. - Hugs and Kisses, " + gameObject.name);
			Destroy(gameObject);
		}

		AnimationBools.Add("Running", false);
		AnimationBools.Add("Shooting", false);
		AnimationBools.Add("Jumping", false);
	}

	void Update () {
		// ----- Get the input for movement
		int hor = 0;
		int ver = 0;
		if (Input.GetKey(KeyCode.LeftArrow))  { hor -= 1; }
		if (Input.GetKey(KeyCode.RightArrow)) { hor += 1; }
		if (Input.GetKey(KeyCode.DownArrow))  { ver -= 1; }
		if (Input.GetKey(KeyCode.UpArrow))    { ver += 1; }

		// ----- Reset animation states
		ResetAnimationStates();

		// ----- Apply movement
		_movement.Move(hor);
		if (hor != 0) {
			AnimationBools["Running"] = true;
		}
		if (ver != 0) {
			transform.position += ver * _movement.Speed * Time.deltaTime * transform.up; // vertical movement for debugging
			AnimationBools["Jumping"] = true;
		}


		// ----- Shoot stuff
		if (Input.GetKey(KeyCode.Z)) {
			_shooter.Shoot(_movement.Direction);
			AnimationBools["Shooting"] = true;
		}

		HandleAnimation();
	}

	public void ResetAnimationStates() {
		AnimationBools["Running"]  = false;
		AnimationBools["Shooting"] = false;
		AnimationBools["Jumping"]  = false;
	}

	public void HandleAnimation() {
		// ----- Basic animation handling. Let's do this better later.
		_anim.SetBool("Running",  AnimationBools["Running"]);
		_anim.SetBool("Shooting", AnimationBools["Shooting"]);
		_anim.SetBool("Jumping",  AnimationBools["Jumping"]);
	}
}

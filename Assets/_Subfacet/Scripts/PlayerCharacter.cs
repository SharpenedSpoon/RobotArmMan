using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCharacter : MonoBehaviour {

	private Movement _movement = null;
	private Health _health = null;
	private Shooter _shooter = null;

	private bool isStunned = false;
	private float endStunTime = 0.0f;

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
		AnimationBools.Add("Falling", false);
    }

	void Update () {
		// ----- Get the input for movement
		int hor = 0;
		int ver = 0;

		if (!isStunned) {
			if (Input.GetKey(KeyCode.LeftArrow))  { hor -= 1; }
			if (Input.GetKey(KeyCode.RightArrow)) { hor += 1; }
			//if (Input.GetKey(KeyCode.DownArrow))  { ver -= 1; }
			if (Input.GetKeyDown(KeyCode.UpArrow))    { ver += 1; }
		} else {
			// check to see if we're not stunned anymore
			if (Time.time >= endStunTime) {
				Unstun();
			}
		}

		// ----- Reset animation states
		ResetAnimationStates();

		// ----- Apply movement
		_movement.SetMovementVariables(hor, ver);

		// ----- Shoot stuff
		if (Input.GetKey(KeyCode.Z)) {
			_shooter.Shoot(_movement.Direction);
			AnimationBools["Shooting"] = true;
		}

		// ----- Figure out and pass the animation bools
		HandleAnimation();
	}

	public void Stun() {
		Stun(1.0f);
	}
	
	public void Stun(float stunTime) {
		isStunned = true;
		endStunTime = Time.time + stunTime;
		_anim.SetBool("Stunned", true);
	}
    
    public void Unstun() {
		isStunned = false;
		_anim.SetBool("Stunned", false);
    }
    
	public void ResetAnimationStates() {
		AnimationBools["Running"]  = false;
		AnimationBools["Shooting"] = false;
		AnimationBools["Jumping"]  = false;
		AnimationBools["Falling"]  = false;
	}

	public void HandleAnimation() {
		float velX = rigidbody2D.velocity.x;
		float velY = rigidbody2D.velocity.y;

		if (_movement.HorizontalMovement != 0) {
			AnimationBools["Running"] = true;
		}

		if (Mathf.Abs(velY) > 0.2) {
			if (velY > 0) {
				AnimationBools["Jumping"] = true;
			} else if (velY < 0) {
				AnimationBools["Falling"] = true;
			}
		}

		_anim.SetBool("Running",  AnimationBools["Running"]);
		_anim.SetBool("Shooting", AnimationBools["Shooting"]);
		_anim.SetBool("Jumping",  AnimationBools["Jumping"]);
		_anim.SetBool("Falling",  AnimationBools["Falling"]);
		_anim.SetBool("OnGround", _movement.OnGround);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCharacter : MonoBehaviour {

	private Movement movement = null;
	private Health health = null;
	private Shooter shooter = null;

	private bool isStunned = false;
	private float endStunTime = 0.0f;

	private Animator anim;
	public Dictionary<string, bool> AnimationBools = new Dictionary<string, bool>();


	void Start () {
		movement = GetComponent<Movement>();
		health = GetComponent<Health>();
		shooter = GetComponent<Shooter>();
		anim = GetComponent<Animator>();
		if (movement == null || health == null || shooter == null || anim == null) {
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
		movement.SetMovementVariables(hor, ver);

		// ----- Shoot stuff
		if (Input.GetKey(KeyCode.Z)) {
			shooter.Shoot(movement.direction);
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
		anim.SetBool("Stunned", true);
	}
    
    public void Unstun() {
		isStunned = false;
		anim.SetBool("Stunned", false);
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

		if (movement.horizontalMovement != 0) {
			AnimationBools["Running"] = true;
		}

		if (Mathf.Abs(velY) > 0.2) {
			if (velY > 0) {
				AnimationBools["Jumping"] = true;
			} else if (velY < 0) {
				AnimationBools["Falling"] = true;
			}
		}

		anim.SetBool("Running",  AnimationBools["Running"]);
		anim.SetBool("Shooting", AnimationBools["Shooting"]);
		anim.SetBool("Jumping",  AnimationBools["Jumping"]);
		anim.SetBool("Falling",  AnimationBools["Falling"]);
		anim.SetBool("OnGround", movement.onGround);
	}
}

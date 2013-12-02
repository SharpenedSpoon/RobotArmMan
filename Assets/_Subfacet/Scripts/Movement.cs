﻿using UnityEngine;
using System.Collections;
using Vectrosity;

public class Movement : MonoBehaviour {
	
	public float Speed = 10.0f;
	public float JumpForce = 500.0f;
	public bool OnGround = true;
	
	[HideInInspector]
	public bool IsFacingRight = true;
	[HideInInspector]
	public int Direction = 1;
	private RaycastHit2D hit;

	public bool IsFalling { get; private set; }
	public bool IsJumping { get; private set; }

	void Start() {
		IsFalling = false;
		IsJumping = false;
	}

	void Update() {
		CheckOnGround();
	}

	public void CheckOnGround() {
		OnGround = false;
		Vector3 endPoint = transform.position - (0.85f * transform.up);
		if (Physics2D.Linecast(transform.position, endPoint, ~(1 << gameObject.layer) )) {
			OnGround = true;
			IsFalling = false;
			IsJumping = false;
		}

		DebugDrawGroundCheck(endPoint);
	}

	public void Move(int horizontalDirection) {
		if (horizontalDirection == 0) {
			return;
		}
		
		// ----- Figure out which way we're facing
		if (horizontalDirection < 0) {
			FaceLeft();
		} else if (horizontalDirection > 0) {
			FaceRight();
		}

		// ----- Apply movement
		transform.position += horizontalDirection * Speed * Time.deltaTime * transform.right;
	}

	public void Jump(int ver) {
		if (ver == 1 && OnGround) {
			rigidbody2D.AddForce(500.0f * transform.up);
		}
	}

	public void Flip() {
		IsFacingRight = IsFacingRight ? false : true;
		Direction = IsFacingRight ? 1 : -1;
		transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
	
	public void FaceLeft() {
		if (IsFacingRight) {
			Flip();
		}
	}
	
	public void FaceRight() {
		if (!IsFacingRight) {
			Flip();
		}
	}

	public void DebugDrawGroundCheck(Vector3 endPoint) {
		Color groundColor = Color.red;
		if (Physics2D.Linecast(transform.position, endPoint, ~(1 << gameObject.layer) )) {
			groundColor = Color.green;
		}
		var blah = new Vector3[2];
		blah[0] = transform.position + transform.right;
		blah[1] = endPoint + transform.right;
		Vectrosity.VectorLine.SetLine3D(groundColor, 0.01f, blah);
	}
}

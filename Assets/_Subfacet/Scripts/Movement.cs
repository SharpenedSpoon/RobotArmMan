using UnityEngine;
using System.Collections;
using Vectrosity;

public class Movement : MonoBehaviour {
	
	public float speed = 10.0f;
	public float jumpForce = 550.0f;
	public bool onGround = true;

	public bool standOnOwnLayer = false;
	
	[HideInInspector]
	public bool isFacingRight = true;
	[HideInInspector]
	public int direction = 1;
	[HideInInspector]
	public int horizontalMovement = 0;
	[HideInInspector]
	public bool canJump = false;
	[HideInInspector]
	public bool jump = false;
	private RaycastHit2D hit;

	public bool isFalling { get; private set; }
	public bool isJumping { get; private set; }

	void Start() {
		isFalling = false;
		isJumping = false;
	}

	void FixedUpdate() {
		if (horizontalMovement != 0) {
			//rigidbody2D.AddForce(Vector2.right * HorizontalMovement * moveForce);
			Move(horizontalMovement);
		}
		if (jump && canJump) {
			rigidbody2D.AddForce(new Vector2(0.0f, jumpForce));
			canJump = false;
			jump = false;
		}
	}

	void Update() {
		CheckOnGround();
	}

	public void CheckOnGround() {
		onGround = false;
		canJump = false;
		Vector3 endPoint = transform.position - (0.85f * transform.up);

		if (standOnOwnLayer) {
			int realLayer = gameObject.layer;
			gameObject.layer = 2; // layer 2 is the "ignore raycast" layer
			hit = Physics2D.Linecast(transform.position, endPoint, ~(1 << gameObject.layer));
			gameObject.layer = realLayer;

			if (hit && hit.collider.gameObject == gameObject) {
				Debug.Log ("quit hitting yourself!");
				standOnOwnLayer = false;
			}

		} else {
			hit = Physics2D.Linecast(transform.position, endPoint, ~(1 << gameObject.layer));
		}
		if (hit) {
			onGround = true;
			canJump = true;
			isFalling = false;
			isJumping = false;
		}

		DebugDrawGroundCheck(endPoint);
	}

	public void SetMovementVariables(int hor, int ver) {
		horizontalMovement = hor;
		if (ver == 1) {
			jump = true;
		} else {
			jump = false;
		}
	}

	public void Move(int horizontalDirection) {
		if (horizontalDirection == 0) {
			//return; // we don't want to return if we are telling rigidbody2d what our velocity is
		}
		
		// ----- Figure out which way we're facing
		if (horizontalDirection < 0) {
			FaceLeft();
		} else if (horizontalDirection > 0) {
			FaceRight();
		}

		// ----- Apply movement
		//transform.position += horizontalDirection * Speed * Time.deltaTime * transform.right;
		rigidbody2D.velocity = new Vector2(horizontalDirection * speed, rigidbody2D.velocity.y);
	}

	public void DoJump(int ver) {
		if (ver == 1) {
			//rigidbody2D.AddForce(500.0f * transform.up);
			//rigidbody2D.velocity += new Vector2(0.0f, 10.0f);
			onGround = false;
		}
	}

	public void Flip() {
		isFacingRight = isFacingRight ? false : true;
		direction = isFacingRight ? 1 : -1;
		transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
	
	public void FaceLeft() {
		if (isFacingRight) {
			Flip();
		}
	}
	
	public void FaceRight() {
		if (!isFacingRight) {
			Flip();
		}
	}

	public void DebugDrawGroundCheck(Vector3 endPoint) {
		Color groundColor = Color.red;
		if (onGround) {
			groundColor = Color.green;
		}
		var blah = new Vector3[2];
		blah[0] = transform.position + transform.right;
		blah[1] = endPoint + transform.right;
		Vectrosity.VectorLine.SetLine3D(groundColor, 0.01f, blah);
	}
}

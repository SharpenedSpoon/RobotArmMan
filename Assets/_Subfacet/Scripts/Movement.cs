using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	
	public float Speed = 10.0f;
	[HideInInspector]
	public bool IsFacingRight = true;
	[HideInInspector]
	public int Direction = 1;

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
}

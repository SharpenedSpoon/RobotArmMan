using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[HideInInspector]
	public bool isFacingRight = true;
	public float speed = 1.0f;

	void Start () {
	
	}

	void Update () {

		// ----- Get the input
		int hor = 0;
		int ver = 0;
		if (Input.GetKey(KeyCode.LeftArrow))  { hor -= 1; }
		if (Input.GetKey(KeyCode.RightArrow)) { hor += 1; }
		if (Input.GetKey(KeyCode.DownArrow))  { ver -= 1; }
		if (Input.GetKey(KeyCode.UpArrow))    { ver += 1; }

		// ----- Figure out which way we're facing
		if (hor < 0) {
			FaceLeft();
		} else if (hor > 0) {
			FaceRight();
		}

		// ----- Apply movement
		transform.position += hor * speed * Time.deltaTime * transform.right;
		transform.position += ver * speed * Time.deltaTime * transform.up;
	}

	public void Flip() {
		isFacingRight = isFacingRight ? false : true;
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
}

using UnityEngine;
using System.Collections;
using Vectrosity;

public class GameCharacter : MyGameObject {
	
	public float RateOfFire = 1.0f;
	public Rigidbody2D bullet = null;
	public int Health = 10;

	public float scl = 0.65f;

	private float _shootTimer = 0.0f;
	
	public virtual void Update() {
		_shootTimer += Time.deltaTime;

		if (Health <= 0) {
			Die();
		}

		var test = new Vector3[4];
		test[0] = transform.position;
		test[1] = transform.position + scl*Direction*transform.right;
		test[2] = transform.position + scl*Direction*transform.right + new Vector3(0.0f,0.2f,0.0f);
		test[3] = transform.position + scl*Direction*transform.right + new Vector3(0.0f,-0.2f,0.0f);
		Vectrosity.VectorLine.SetLine3D(Color.green, 0.01f, test);
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

	public void Shoot() {
		if (bullet != null && _shootTimer >= RateOfFire) {
			_shootTimer = 0.0f;

			// ----- Spawn bullet
			GameObject thisBullet = Instantiate(bullet, transform.position + 0.65f*Direction*transform.right, Quaternion.identity) as GameObject;

			// ----- Match bullet facing to player
			if (thisBullet != null) {
				var thisBulletScript = thisBullet.GetComponent<Bullet>();
				if (thisBulletScript != null) {
					thisBulletScript.OwnerTag = gameObject.tag;
					thisBulletScript.IsFacingRight = IsFacingRight;
				}
			}
		}
	}

	public void TakeDamage(int dmg) {
		Health -= dmg;
	}
}

using UnityEngine;
using System.Collections;
using Vectrosity;

public class Shooter : MonoBehaviour {

	public float scl = 0.65f;
	public float RateOfFire = 1.0f;
	public Rigidbody2D bullet = null;

	private float _shootTimer = 0.0f;
	private Movement _movement = null;

	void Start() {
		_movement = GetComponent<Movement>();
	}


	void Update () {
		_shootTimer += Time.deltaTime;

		// ----- Draw from whence the shot will come
		if (_movement != null) {
				var test = new Vector3[4];
				test[0] = transform.position;
				test[1] = transform.position + scl*_movement.Direction*transform.right;
				test[2] = transform.position + scl*_movement.Direction*transform.right + new Vector3(0.0f,0.2f,0.0f);
				test[3] = transform.position + scl*_movement.Direction*transform.right + new Vector3(0.0f,-0.2f,0.0f);
				Vectrosity.VectorLine.SetLine3D(Color.green, 0.01f, test);
		}
	}

	public void Shoot(int dir = 1) {
		if (bullet != null && _shootTimer >= RateOfFire) {
			_shootTimer = 0.0f;
			
			// ----- Spawn bullet
			GameObject thisBullet = Instantiate(bullet, transform.position + 0.65f*dir*transform.right, Quaternion.identity) as GameObject;
			
			// ----- Match bullet facing to player
			if (thisBullet != null) {
				var thisBulletScript = thisBullet.GetComponent<Bullet>();
				if (thisBulletScript != null) {
					thisBulletScript.ShootInDirection(dir);
					thisBulletScript.OwnerTag = gameObject.tag;
                }
            }
        }
    }
}

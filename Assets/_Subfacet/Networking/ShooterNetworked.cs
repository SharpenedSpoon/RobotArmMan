using UnityEngine;
using System.Collections;

public class ShooterNetworked : MonoBehaviour {

	public float sclRight = 0.69f;
	public float sclUp = 0.09f;
	public float RateOfFire = 0.2f;
	public GameObject bullet = null;
	
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
			test[0] = transform.position + sclUp*transform.up;
			test[1] = transform.position + sclUp*transform.up + sclRight*_movement.direction*transform.right;
			test[2] = transform.position + sclUp*transform.up + sclRight*_movement.direction*transform.right + new Vector3(0.0f,0.2f,0.0f);
			test[3] = transform.position + sclUp*transform.up + sclRight*_movement.direction*transform.right + new Vector3(0.0f,-0.2f,0.0f);
			Vectrosity.VectorLine.SetLine3D(Color.green, 0.01f, test);
		}
	}
	
	public void Shoot(int dir) {
		if (bullet != null && _shootTimer >= RateOfFire) {
			_shootTimer = 0.0f;
			
			// ----- Spawn bullet
			GameObject thisBullet = PhotonNetwork.Instantiate("BulletNetworked", transform.position + 0.69f*dir*transform.right + 0.09f*transform.up, Quaternion.identity, 0) as GameObject;
			BulletNetworked bulletScript = thisBullet.GetComponent<BulletNetworked>();
			if (bulletScript != null) {
                //bulletScript.Direction = _movement.Direction;
                bulletScript.ShootInDirection(_movement.direction);
                bulletScript.OwnerTag = gameObject.tag;
            }
        }
    }
}

﻿using UnityEngine;
using System.Collections;

public class BulletNetworked : MonoBehaviour {

	public int Damage = 1;
	public float Lifetime = 20.0f;
	public bool DestroyOnHit = true;
	public float Speed = 20.0f;
	
	[HideInInspector]
	public string OwnerTag = "Player";
	[HideInInspector]
	public int Direction = 0;
	[HideInInspector]
	public bool FacingRight;
	
	private float _lifeTimer = 0.0f;
	private HealthNetworked _health = null;
	
	void Start () {
		_health = GetComponent<HealthNetworked>();
	}
	
	void Update () {
		_lifeTimer += Time.deltaTime;
		if (_lifeTimer >= Lifetime) {
			_health.Die();
		}
	}
	
	public void ShootInDirection(int dir) {
		rigidbody2D.velocity = transform.right * Speed * dir;
	}
	
	public void SetFacingRight(bool facingRightInput) {
		FacingRight = facingRightInput;
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		//if (!col.gameObject.CompareTag(OwnerTag)) {
			if (col.gameObject != null) {
				var healthScript = col.gameObject.GetComponent<HealthNetworked>();
                if (healthScript != null) {
                    healthScript.TakeDamage(1);
                }
            }
            if (DestroyOnHit) {
                _health.Die();
            }
        //}
    }
}

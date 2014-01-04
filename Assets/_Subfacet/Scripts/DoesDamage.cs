using UnityEngine;
using System.Collections;

public class DoesDamage : MonoBehaviour {

	public int damage = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col) {
		/*if (col.gameObject != gameObject) {
			//if (!col.gameObject.CompareTag(OwnerTag)) {
			if (col.gameObject != null) {
				var healthScript = col.gameObject.GetComponent<Health>();
				if (healthScript != null) {
					healthScript.TakeDamage(1);
				}
				if (stunOnHit) {
					col.gameObject.SendMessage("Stun", stunTimeMilliseconds, SendMessageOptions.DontRequireReceiver);
                }
            }
            //}
            
            if (DestroyOnHit) {
                _health.Die();
            }
        }*/
    }
}

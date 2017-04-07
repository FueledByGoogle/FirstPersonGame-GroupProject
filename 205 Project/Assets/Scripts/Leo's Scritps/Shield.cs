using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	AudioSource shieldHitAudio;
	public float shieldDefenseValue;

	public ParticleSystem shieldSpark;						
	public float shieldCoolDown = 2f;	//Time before shield can be used again after failing to block damage


	void Start() {
		shieldHitAudio = GetComponent<AudioSource> ();
	}
	//checks if shield defense value is high enough to negate damage.
	public bool TakeDamage (float damage) {
		shieldHitAudio.Play ();
		if (damage <= shieldDefenseValue) {
			return false;
		} else {
			return true;
		}
	}

	void OnTriggerEnter (Collider coll) {

		if (coll.tag == "Weapon") {
			shieldSpark.Stop ();
			shieldSpark.Play ();
		}
	}

}

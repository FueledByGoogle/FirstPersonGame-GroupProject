using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	AudioSource shieldHitAudio;
	public float shieldDefenseValue;
	public bool shieldHit;

	public ParticleSystem shieldSpark;						
	public float shieldCoolDown = 2f;	//Time before shield can be used again after failing to block damage


	void Start() {
		shieldHit = false;
		shieldHitAudio = GetComponent<AudioSource> ();
	}

	public bool TakeDamage (float damage) {
		shieldHitAudio.Play ();
		if (damage <= shieldDefenseValue) {	//no damage should be taken if damage < shieldDefenseValue
			return false;
		} else {
			return true;
		}
	}

	void OnTriggerEnter (Collider coll) {
		shieldHit = true;

		if (coll.tag == "Weapon") {
			shieldSpark.Stop ();
			shieldSpark.Play ();
		}
	}

	void OnTriggerExit (Collider coll) {
		shieldHit = false;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	AudioSource shieldHitAudio;
	public float shieldDefenseValue;
	//Different shields will have different cool down after failing to block damage
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

}

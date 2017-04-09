using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	public Character character;

	AudioSource shieldHitAudio;
	public float shieldDefenseValue;

	public ParticleSystem shieldSpark;						
	public float shieldCoolDown = 2f;	//Time before shield can be used again after failing to block damage


	void Start() {
		character = transform.root.GetComponent<Character> ();
	
		shieldHitAudio = GetComponent<AudioSource> ();
	}


	public bool TakeDamage (float damage) {
		if (shieldHitAudio.isPlaying)
			shieldHitAudio.Stop ();
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

			//Below we stop animation of sword swing so sword doesn't pass through shield
			//and start additional triggers
			if (character != null) {
				//I realized I probably should have named the class "Weapon" instead of sword
				//so it would make sense when grabbing the script to see how much damage the
				//weapon does
				Sword swordHit = coll.GetComponent<Sword> ();
				if (swordHit != null && (swordHit.swordDamage <= shieldDefenseValue)) {
					Character tempChar = coll.transform.root.GetComponent<Character> ();
					if (tempChar != null) {
						tempChar.anim.Play ("Idle");
					}

				}
			}

		}
	}

}

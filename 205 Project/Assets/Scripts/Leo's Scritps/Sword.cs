using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	public AudioSource swordSwingAudio;

	public float swordDamage;
	public bool hasCollided;	//used to prevent multiple hits from being registered in one swing
								//hascollided needs to be set to false when not swinging sword

	void Start () {
		hasCollided = false;
	}

	void OnTriggerEnter(Collider col) {

		/* Once sword has collided hasCollided will not be set to true until attack animation is finished.
		 * Set hasCollided to false whenever the attack animation of the sword swing is not playing
		 */

		if (hasCollided == false) {
			
			hasCollided = true;
			Character characterHit = col.gameObject.transform.root.GetComponent<Character> ();

			if (this.gameObject.transform.root.tag != col.gameObject.transform.root.tag) {
				if (characterHit != null) {
					characterHit.TakeDamage (swordDamage);
				}
			}


		}
	}

}

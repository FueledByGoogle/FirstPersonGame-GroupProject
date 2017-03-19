using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	public int swordDamage;
	public AudioSource swordSwingAudio;

	void Start () {
	}

	void OnTriggerEnter(Collider col) {
		Character characterHit = col.gameObject.transform.root.GetComponent<Character> ();

		if (characterHit != null) {
			characterHit.TakeDamage (swordDamage);
		}

	}

	/*TODO: OnCollisionEnter hit effect sparks so player knows
	 * the target has been hit
	 */
}

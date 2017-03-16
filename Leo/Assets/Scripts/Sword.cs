using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	/*	TODO: Sword collider that has trigger enabled is only active
	 * if the mouse animation is down
	 */

	public int swordDamage;

	void OnTriggerEnter(Collider col) {
		Character characterHit = col.gameObject.GetComponent<Character> ();

		if (characterHit != null) {
			characterHit.TakeDamage (swordDamage);
		}

	}
}

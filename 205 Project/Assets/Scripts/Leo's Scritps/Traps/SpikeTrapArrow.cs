using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapArrow : MonoBehaviour {

	public float damage = 1f;

	public void OnTriggerEnter (Collider col) {
		Character characterHit = col.gameObject.transform.root.GetComponent<Character> ();

		if (characterHit != null) {
			characterHit.TakeDamage (damage);
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapArrow : MonoBehaviour {

	float damage = 1f;

	void OnTriggerEnter(Collider coll){

		Character characterHit = coll.gameObject.transform.root.GetComponent<Character> ();

		if (characterHit != null) {
			characterHit.TakeDamage (damage);
		}
	}


}

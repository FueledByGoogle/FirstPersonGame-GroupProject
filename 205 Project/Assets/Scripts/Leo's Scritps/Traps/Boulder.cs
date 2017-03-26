using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour {

	public const float damage = 5;
	public Collider boulderCollider;

	void OnTriggerEnter (Collider col) {

		/*Need to use transform.root because hitting the character in places
		* where the script isn't placed will return a null unless we traverse to where
		* the script is located. In this the Player gameobject
		*/
		Character characterHit = col.gameObject.transform.root.GetComponent<Character> ();

		if (characterHit != null && boulderCollider.isTrigger == true) {
			characterHit.TakeDamage (damage);
		}

		boulderCollider.isTrigger = false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	public int swordDamage;

	void OnTriggerEnter(Collider other) {
		if (Input.GetMouseButton (0)) {	//only be able to hurt enemy if player is swinging the sword
			if (other.name == "Enemy") {
				Character enemy = other.GetComponent<Character> ();
				enemy.takeDamage (swordDamage);
			}
		}
	}
}

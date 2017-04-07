using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDeathTrap : MonoBehaviour {

	private PlayerController player;

	void Start () {
		player = GameObject.Find ("MyCustomPlayer").GetComponent<PlayerController> ();
	}

	void OnCollisionEnter (Collision coll) {
		if (coll.gameObject.transform.root.CompareTag ("Player") && player != null) {
			player.character.TakeDamage (player.character.maxHealth);
		}
	}

}

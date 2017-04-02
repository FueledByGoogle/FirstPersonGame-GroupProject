using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionsScript : MonoBehaviour {

	public float reGainXHealth = 5;
	public Character player;
	
	void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.CompareTag("Player")){
			player.GainHealth(reGainXHealth);
			Destroy(gameObject);
		}        
    }
}

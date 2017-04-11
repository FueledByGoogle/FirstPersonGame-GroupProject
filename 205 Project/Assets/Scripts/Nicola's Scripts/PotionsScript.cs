using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionsScript : MonoBehaviour {

	public float reGainXHealth = 5f;
	public Character player;

	void Start(){
		player = GameObject.Find("MyCustomPlayer").GetComponent<Character>() ;
	}

	void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.CompareTag("Player")){
			player.GainHealth(reGainXHealth);
			Destroy(gameObject);
		}        
    }
}

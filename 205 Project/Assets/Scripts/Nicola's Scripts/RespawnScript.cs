using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*This piece of code is attached to a boundary object
so, when something with the tag 'Player' collides with
it, it returns to the starting point

public Rigidbody player   ====   the object with the 'Player' tag
*/



public class RespawnScript : MonoBehaviour {


	public Rigidbody player;
	public Vector3 place;


	// Use this for initialization
	void Start () {
		place = player.transform.position;
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.CompareTag("Player")){
			player.isKinematic = true;
			player.velocity = new Vector3(0,0,0);
			player.transform.position = place;
			player.isKinematic = false;
		}        
    }
}

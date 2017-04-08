using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpScript : MonoBehaviour {
	
	public GameObject WarpTo;
	public RespawnScript respawnPoint;
	public Rigidbody player;
	
	public GameObject listOfTraps;
	public GameObject prevTraps;
	


	// Use this for initialization
	void Start () {

	}
	
	void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.CompareTag("Player") /*&& rend == true*/){
			player.isKinematic = true;
			player.velocity = new Vector3(0,0,0);
			player.transform.position = WarpTo.transform.position;
			player.isKinematic = false;
			respawnPoint.place = WarpTo.transform.position;
			listOfTraps.SetActive(true);
			prevTraps.SetActive(false);
		}        
    }
}

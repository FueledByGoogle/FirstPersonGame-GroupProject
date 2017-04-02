using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpScript : MonoBehaviour {
	
	public GameObject WarpTo;
	public RespawnScript respawnPoint;
	public Rigidbody player;
	
	private Renderer rend;
	
	//Vector3 StartWarp;
	Vector3 FinishWarp;

	// Use this for initialization
	void Start () {
		//StartWarp = gameObject.transform.position;
		FinishWarp = WarpTo.transform.position;
		rend = GetComponent<Renderer>();
	}
	
	void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.CompareTag("Player") && rend == true){
			player.isKinematic = true;
			player.velocity = new Vector3(0,0,0);
			player.transform.position = FinishWarp;
			player.isKinematic = false;
			respawnPoint.place = FinishWarp;
		}        
    }
}

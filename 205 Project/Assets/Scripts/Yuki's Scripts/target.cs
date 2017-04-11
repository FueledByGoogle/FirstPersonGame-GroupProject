using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour {

	public floorGenerator roomMan;

	// Use this for initialization
	void Start () {
		roomMan = GameObject.Find ("DoorWallEntrance").GetComponent<floorGenerator> ();
	}
		
	public void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Weapon") {
			roomMan.targetHit ();
			Destroy (gameObject);
		}
	}
}

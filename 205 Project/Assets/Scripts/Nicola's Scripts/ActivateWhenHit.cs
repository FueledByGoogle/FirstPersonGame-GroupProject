using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWhenHit : MonoBehaviour {

	public GameObject Traps;
	public bool forExitRoom;
	public puzzleRoomMan roomMan;
		
	void OnTriggerEnter (Collider col) {
		if (!forExitRoom) {
			Traps.SetActive (true);   
		} else {
			if (col.gameObject.tag == "Weapon") {
				roomMan.hitTarget ();
				Destroy (gameObject);
			}
		}		   
    }
}

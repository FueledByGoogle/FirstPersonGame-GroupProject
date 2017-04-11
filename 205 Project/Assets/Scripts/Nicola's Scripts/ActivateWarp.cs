using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWarp : MonoBehaviour {

	public GameObject Traps;
	public puzzleRoomMan roomMan;
		
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Weapon") {
			roomMan.hitTarget ();
			Destroy (gameObject);
			Traps.SetActive(true);
		}		   
    }
}

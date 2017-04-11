using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleRoomMan : MonoBehaviour {

	public int numTargets;
	public Door door;
	// Use this for initialization

	public void hitTarget(){
		numTargets--;
		if(numTargets <= 0){
			door.cleared = true;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRoomManager : MonoBehaviour {


	public int numOfEnemies;
	public Door door;

	void Update () {
		if (numOfEnemies <= 0) {
			door.cleared = true;
		}
	}
}

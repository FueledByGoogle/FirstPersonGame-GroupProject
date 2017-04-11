using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRoomManager : MonoBehaviour {

	private PlayerController player;
	private GameObject[] spawnedNPC;
	private EnemyAI[] spawnedNPCScript;

	public Transform[] points;
	public GameObject AI;
	public int numOfEnemies;
	public Door door;

	private int numOfEnemiesToSpawn;
	private bool seen;

	void Start() {
		
		player = GameObject.Find("MyCustomPlayer").GetComponent<PlayerController> ();

		if(player == null)
			print ("player is null");

		if (player != null) {
			numOfEnemies = player.roomsCleared * 2;
			if (numOfEnemies > 5) {
				numOfEnemies = 5;

			}
			numOfEnemiesToSpawn = numOfEnemies;
			spawnedNPC = new GameObject[numOfEnemies];
			spawnedNPCScript = new EnemyAI[numOfEnemies];	//need this to set waypoints
			//we want to spawn enemies every 2 seconds
			for (int i = 0; i < numOfEnemies; i++) {
				spawnedNPC[i] = Instantiate (AI, transform.position, transform.rotation) as GameObject;
				spawnedNPCScript [i] = spawnedNPC [i].GetComponent<EnemyAI> ();
				spawnedNPCScript [i].points [0] = points [0];
				spawnedNPCScript [i].points [1] = points [1];
				spawnedNPC [i].SetActive (false);	//deactivating them so we can spawn them later in update in intervals
			}
		}
	}

	void Update () {


		if (numOfEnemies <= 0) {
			door.cleared = true;
		}


		StartCoroutine(Wait());


		if (!seen) {	//if one npc gets hit then all npcs currently spawned will go after player
			for (int i = 0; i < numOfEnemiesToSpawn; i++) {
				if (spawnedNPCScript [i].inLineSight) {
					seen = true;
					break;
				}
			}
		}

		if (seen) {
			for (int i = 0; i < numOfEnemiesToSpawn; i++) {
				spawnedNPCScript [i].inLineSight = true;
			}
		}
	}


	IEnumerator Wait() {
		for (int i = 0; i < numOfEnemiesToSpawn; i++) {
			if (!spawnedNPC [i].activeSelf) {
				spawnedNPC [i].SetActive (true);
			}
			yield return new WaitForSeconds (3);
		}
	}
}

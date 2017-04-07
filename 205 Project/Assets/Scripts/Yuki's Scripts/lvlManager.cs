using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvlManager : MonoBehaviour {
	
	public GameObject spawnPoint;
    public lvlManager lvlMan;

	private GameObject player;

    void Start () {
		player = GameObject.Find("MyCustomPlayer");
		spawnPoint = GameObject.Find ("spawnPoint");
		player.transform.position = spawnPoint.transform.position;
		player.transform.rotation = spawnPoint.transform.rotation;
	}

}

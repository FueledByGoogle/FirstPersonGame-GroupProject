using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvlManager : MonoBehaviour {

    roomNode start;
    public lvlManager lvlMan;
	private GameObject player;
	public GameObject spawnPoint;

    // Use this for initialization
    void Start () {
		player = GameObject.Find("MyCustomPlayer");
		//spawnPoint = GameObject.Find ("spawnPoint");
		player.transform.position = spawnPoint.transform.position;
		player.transform.rotation = spawnPoint.transform.rotation;
		/*
		 * 
		 * 
		 * 
		*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Awake()
    {
        if (lvlMan == null)
        {
            lvlMan = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (lvlMan != this)
        {
            Destroy(gameObject);
        }
			

    }
}

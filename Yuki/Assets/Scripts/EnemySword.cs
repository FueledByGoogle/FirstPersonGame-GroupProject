using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour {


    public GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "player")
        {
            //deal damage
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "player")
        {

        }
    }
}

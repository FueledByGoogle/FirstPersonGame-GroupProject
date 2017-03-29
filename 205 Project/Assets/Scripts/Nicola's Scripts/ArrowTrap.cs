using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour {
	
	bool isActivated;
	int fired;
	public GameObject Circle;
	
	//public Rigidbody plateTrap;
	
	float startTime;
	
	public Rigidbody arrowPrefab;
		
	void Start () {
		isActivated = false;
		fired = 0;
		startTime = Time.time;
	}

	void Update () {
		if(isActivated == true && fired == 0){
			fired = 1;
		}		
		if(Time.time > startTime + 3f && fired == 1){
			fired = 0;
			startTime = Time.time;
		}
		else if(Time.time > startTime + 5f){
			startTime = Time.time;
		}
	}
	
	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.transform.root.tag == "Player"){
			isActivated = true;
		}
	}
	
	void OnTriggerExit(Collider coll){
		if (coll.gameObject.transform.root.tag == "Player"){
			isActivated = false;
			fired = 0;
		}
	}
}

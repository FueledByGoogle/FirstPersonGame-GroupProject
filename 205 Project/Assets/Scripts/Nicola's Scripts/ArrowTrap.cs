using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour {
	
	bool isActivated;
	int fired;
	public GameObject Circle;
	
	public Rigidbody plateTrap;
	
	public Rigidbody arrowPrefab;
		
	// Use this for initialization
	void Start () {
		isActivated = false;
		fired = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(isActivated == true && fired == 0){
			var newArrow = Instantiate (arrowPrefab,Circle.transform.position,transform.rotation);
			fired = 1;
		}		
	}
	
	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.CompareTag("Player")){
			isActivated = true;
		}

		
	}
	
	void OnTriggerExit(Collider coll){
		if (coll.gameObject.CompareTag("Player")){
			isActivated = false;
			fired = 0;
		}
	}
}

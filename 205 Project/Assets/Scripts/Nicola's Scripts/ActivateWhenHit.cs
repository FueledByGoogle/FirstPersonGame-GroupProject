using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWhenHit : MonoBehaviour {

	public GameObject Traps;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnCollisionEnter(Collision coll) {
		Traps.SetActive(true);      
    }
}

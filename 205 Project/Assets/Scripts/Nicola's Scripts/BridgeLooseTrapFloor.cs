using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeLooseTrapFloor : MonoBehaviour {

	public float lifeTime = 3.0f;
	float stopTime = 7.0f;
	float startTime;
	bool passBy;
	
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		passBy = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(passBy == true){
			if(Time.time > startTime + lifeTime && Time.time < startTime + lifeTime + stopTime){
				gameObject.transform.position += transform.up * Time.deltaTime * (-0.5f);
			}
			else if(Time.time > startTime + lifeTime + stopTime){
				passBy = false;
			}
		}
	}
	
	
	void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.CompareTag("Player") && passBy == false){
			passBy = true;
			startTime = Time.time;
		}        
    }
}

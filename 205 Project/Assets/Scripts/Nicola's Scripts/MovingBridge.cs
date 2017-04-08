using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBridge : MonoBehaviour {

	public float lifeTime = 3.0f;
	public float speedTime = 3.0f;
	float startTime;
	Vector3 startPosition;
	
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		startPosition = gameObject.transform.position;
	}
	
	
	// Update is called once per frame
	void Update () {
		if(Time.time > startTime + lifeTime && Time.time < startTime + lifeTime){
			gameObject.transform.position -= transform.right * Time.deltaTime * speedTime;
		}
		else if(Time.time > startTime + lifeTime){
			gameObject.transform.position = startPosition;
			startTime = Time.time;
		}
	}
	
}

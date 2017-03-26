using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapScriptBurstArrows : MonoBehaviour {

	float startTime = 0;
	float lifeTime = 3f;
	float negativeOrPositive = 1;
	Vector3 origin;

	// Use this for initialization
	void Start () {
		origin = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Time.time > startTime + lifeTime){
			negativeOrPositive = negativeOrPositive * (-1);
			startTime = Time.time;
		}
		else{
			if(negativeOrPositive == 1){
				gameObject.transform.position = origin + new Vector3(0f, 0.1f, 0f);
			}
			else if(negativeOrPositive == -1){
				gameObject.transform.position = origin + new Vector3(0f, -0.1f, 0f);
			}
		}
	}
}

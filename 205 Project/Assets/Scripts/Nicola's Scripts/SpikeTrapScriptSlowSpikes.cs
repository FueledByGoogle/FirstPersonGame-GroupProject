using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapScriptSlowSpikes : MonoBehaviour {

	float startTime = 0;
	float lifeTime = 2f;
	float negativeOrPositive = 1;
	
	// Update is called once per frame
	void Update () {
		
		if(Time.time > startTime + lifeTime){
			negativeOrPositive = negativeOrPositive * (-1);
			startTime = Time.time;
		}
		else{
			gameObject.transform.position += transform.up * Time.deltaTime * negativeOrPositive;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTrigger : MonoBehaviour {

	public bool TargetHit;

	void Start () {
		TargetHit = false;
	}
	
	void OnTriggerEnter () {
		TargetHit = true;
	}
}

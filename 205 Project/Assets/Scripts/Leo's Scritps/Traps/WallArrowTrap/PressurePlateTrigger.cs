using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateTrigger : MonoBehaviour {

	public Collider boxCollider;
	public GameObject[] arrowSpawn;

	void OnTriggerEnter (Collider col){
		boxCollider.enabled = false;
		foreach (GameObject element in arrowSpawn) {
			element.SetActive (true);
		}
	}

}
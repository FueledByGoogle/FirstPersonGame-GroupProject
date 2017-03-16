using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderTrigger : MonoBehaviour {

	public GameObject boulder;
	Rigidbody boulderRigidBody;
	private float boulderTriggered;
	private bool boulderFallen;
	public GameObject boulderIndicator;	//When a boulder falls indicates where it will land

	void Start () {
		boulderTriggered = Random.Range (0.0f, 1.0f);
		boulderRigidBody = boulder.GetComponent<Rigidbody> ();
		boulderFallen = false;
		boulderIndicator.SetActive (false);
	}

	void OnTriggerEnter (Collider col) {
		if (boulderFallen == false) {
			if (col.tag == "Player") {
				if (boulderTriggered >= 0.2f) {
					boulderFallen = true;
					StartCoroutine (Wait());
					boulderRigidBody.isKinematic = false;
				}
			}
		}

	}

	IEnumerator Wait () {
		Debug.Log ("waiting");
		boulderIndicator.SetActive (true);
		yield return new WaitForSeconds (2);
		boulderIndicator.SetActive (false);
	}
}

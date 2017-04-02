using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderTrigger : MonoBehaviour {

	public GameObject boulder;
	public float boulderFallDelay = 2f;
	Rigidbody boulderRigidBody;

	public GameObject boulderIndicator;	//When a boulder falls indicates where it will land
	private float boulderTriggered;
	private bool boulderFallen;

	void Start () {
		boulderTriggered = Random.Range (0, 10);
		boulderRigidBody = boulder.GetComponent<Rigidbody> ();
		boulderFallen = false;
		boulderIndicator.SetActive (false);
	}

	void OnTriggerEnter (Collider col) {
		if (boulderFallen == false) {
			if (col.gameObject.transform.root.tag == "Player") {
				if (boulderTriggered >= 4) {
					boulderFallen = true;
					StartCoroutine (Wait());
					boulderRigidBody.isKinematic = false;
				}
			}
		}
	}

	IEnumerator Wait () {
		boulderIndicator.SetActive (true);
		yield return new WaitForSeconds (boulderFallDelay);
		boulderIndicator.SetActive (false);
	}
}

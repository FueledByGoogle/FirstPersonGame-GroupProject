using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderReset : MonoBehaviour {

	public GameObject boulder;
	public Transform boulderResetPos;

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Boulder") {
			boulder.transform.rotation = new Quaternion (0, 0, 0, 0);
			col.transform.position = boulderResetPos.position;
		}
	}
}

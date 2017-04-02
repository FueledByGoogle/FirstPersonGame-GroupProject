using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderMovement : MonoBehaviour {

	public bool rollRight = true;
	public Rigidbody boulderRigidBody;

	void Start () {
		boulderRigidBody = GetComponent<Rigidbody> ();
	}
	

	void Update () {
		transform.localEulerAngles = new Vector3 (0, 0, transform.localEulerAngles.z);	//manualy freezing x and y rotation because unity freezing doesn't always freeze x and y

		if (rollRight) {
			boulderRigidBody.velocity = new Vector3 (1.5f, boulderRigidBody.velocity.y, 0);
		} else {
			boulderRigidBody.velocity = new Vector3 (-1.5f, boulderRigidBody.velocity.y, 0);
		}
	}
}

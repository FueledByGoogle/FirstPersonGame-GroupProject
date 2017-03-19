using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {



	public const float walkSpeed = 5f;
	public const float turnSpeed = 25f;

	public Rigidbody characterRigidBody;

	void Start () {
		characterRigidBody = GetComponent<Rigidbody> ();

	}
	

	void Update () {
		
		
		if (Input.GetKey (KeyCode.W)) {
			characterRigidBody.transform.position -= transform.right * Time.deltaTime * walkSpeed;
		}
		if (Input.GetKey (KeyCode.D)) {
			characterRigidBody.transform.Rotate(0f, turnSpeed * Time.deltaTime, 0f);
		}
		if (Input.GetKey (KeyCode.S)) {
			characterRigidBody.transform.position += transform.right * Time.deltaTime * walkSpeed;
		}
		if (Input.GetKey (KeyCode.A)) {
			characterRigidBody.transform.Rotate(0f, turnSpeed * Time.deltaTime * (-1), 0f);
		}
		
		
	}
}

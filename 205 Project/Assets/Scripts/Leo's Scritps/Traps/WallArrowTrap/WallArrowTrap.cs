using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallArrowTrap : MonoBehaviour {
	public Rigidbody arrow;
	public float arrowVelocity = 20f;

	void Start () {
		Rigidbody arrowFired = Instantiate (arrow,transform.position, transform.rotation) as Rigidbody;
		arrowFired.velocity = arrowVelocity * transform.forward;
	}


}

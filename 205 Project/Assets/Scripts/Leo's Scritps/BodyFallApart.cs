using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyFallApart : MonoBehaviour {

	public EnemyAI npc;
	private Collider bodyPartcollider;
	private Transform tempParentTransform;
	private Rigidbody bodyRigidBody;

	void Start () {
		bodyRigidBody = GetComponent<Rigidbody> ();
		bodyPartcollider = GetComponent<Collider> ();
	}

	void Update () {
		if (npc.character.health <= 0 && transform.parent != null) {
			/* We have to remember the parent transform because after detaching from parent
			 * it gets teleported somewhere else
			 */
			tempParentTransform = transform.parent;
			transform.parent = null;
			transform.position = tempParentTransform.position;

			bodyRigidBody.isKinematic = false;
			bodyPartcollider.enabled = true;
			if (bodyRigidBody.velocity == Vector3.zero) {
				bodyRigidBody.velocity = new Vector3 (bodyRigidBody.velocity.x, bodyRigidBody.velocity.y + 3f, bodyRigidBody.velocity.z);
			}
		}
	}
}

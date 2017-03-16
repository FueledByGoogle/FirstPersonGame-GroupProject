using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public float maxTravelTime = 5f;
	public float initialTime; 
	public const float damage = 2f;
	public Collider arrowCollider;		//used to make sure arrow doesn't pass through walls on collision

	void Start () {
		initialTime = Time.time;
	}


	void Update () {
		if (Time.time > initialTime + maxTravelTime) {
			Destroy (gameObject);
		}
	}

	public void OnTriggerEnter (Collider col) {

		Character characterHit = col.gameObject.GetComponent<Character> ();

		if (characterHit != null) {
			characterHit.TakeDamage (damage);
		}
		/* Using two colliders rather than disabling the trigger when using only 
		 * one collider after the arrow has hit something to make arrow solid 
		 * fixed the bug where damage is negated if you were only using one arrow
		 * and disabled the trigger after it has hit something.
		 */
		arrowCollider.isTrigger = false;
		Destroy (gameObject, 4f); 
	}

}

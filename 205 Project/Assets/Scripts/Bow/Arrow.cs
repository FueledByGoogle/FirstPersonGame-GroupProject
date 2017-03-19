using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public const float maxTravelTime = 10f;
	public float initialTime; 
	public const float damage = 2f;
	public Collider arrowCollider;
	public Rigidbody arrowRigidbody;

	void Start () {
		initialTime = Time.time;
	}


	void Update () {
		if (Time.time > initialTime + maxTravelTime) {
			Destroy (gameObject);
		}
	}

	public void OnCollisionEnter (Collision col) {
		//we need to traverse to the root of the gameobject because that's where the character
		//script is
		Character characterHit = col.gameObject.transform.root.GetComponent<Character> ();

		if (characterHit != null) {
			characterHit.TakeDamage (damage);
		}
			
		transform.Translate (0.05f * Vector3.back);
		arrowRigidbody.isKinematic = true;
		transform.parent = col.transform;
		Destroy (this.arrowCollider);

	}

}

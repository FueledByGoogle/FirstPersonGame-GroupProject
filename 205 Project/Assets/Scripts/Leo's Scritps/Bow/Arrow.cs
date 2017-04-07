 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public const float maxTravelTime = 5f;
	public float initialTime; 
	public const float damage = 2f;
	public Collider arrowCollider;
	public Rigidbody arrowRigidbody;
	public GameObject arrowTrail;

	AudioSource arrowHit;

	void Start () {
		initialTime = Time.time;
		arrowHit = GetComponent<AudioSource> ();
	}

	void Update () {
		if (Time.time > initialTime + maxTravelTime) {
			Destroy (gameObject);
		}
		transform.forward = Vector3.Lerp (transform.forward, arrowRigidbody.velocity.normalized * 100f, Time.deltaTime);
	}

	public void OnCollisionEnter (Collision coll) {
		//we need to traverse to the root of the gameobject because that's where the character
		//script is

		if (coll.gameObject.tag == "EnvironmentIgnore") {
			Physics.IgnoreCollision (coll.collider, arrowCollider);
		}

		arrowRigidbody.isKinematic = true;
		arrowHit.Play();

		Character characterHit = coll.gameObject.transform.root.GetComponent<Character> ();


		if (characterHit != null) {
			if (characterHit.tag == "Player") {		//we don't want arrow to hang around if it hits the player
				characterHit.TakeDamage (damage);
				Destroy (gameObject);
			} else {								//we want arrow to stick if it hits an enemy
				characterHit.TakeDamage (damage);
				transform.Translate (0.05f * Vector3.forward);	//moves arrow a bit into what it collided with for realism
				transform.parent = coll.transform;
				arrowTrail.SetActive (false);
			}
		} else {							
			Destroy (gameObject, 3f);
		}
			
		Destroy (this.arrowCollider);
	}
		
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public const float maxTravelTime = 10f;
	public float initialTime; 
	public const float damage = 2f;
	public Collider arrowCollider;
	public Rigidbody arrowRigidbody;

	AudioSource arrowHit;

	void Start () {
		initialTime = Time.time;
		arrowHit = GetComponent<AudioSource> ();
	}

	void Update () {
		if (Time.time > initialTime + maxTravelTime) {
			Destroy (gameObject);
		}
	}

	void FixedUpdate() {
		transform.forward = Vector3.Lerp (transform.forward, arrowRigidbody.velocity.normalized, Time.deltaTime);
	}

	public void OnCollisionEnter (Collision col) {
		//we need to traverse to the root of the gameobject because that's where the character
		//script is
		arrowHit.Play();
		Character characterHit = col.gameObject.transform.root.GetComponent<Character> ();

		if (characterHit != null) {
			characterHit.TakeDamage (damage);
		}
			
		transform.Translate (0.05f * Vector3.forward);	//moves arrow a bit into what it collided with for realism
		arrowRigidbody.isKinematic = true;
		transform.parent = col.transform;
		Destroy (this.arrowCollider);
	}
}

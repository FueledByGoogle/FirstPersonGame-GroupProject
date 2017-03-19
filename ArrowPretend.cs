using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPretend : MonoBehaviour {

	public float lifeTime = 3f;
	public float damage = 20;
	public GameObject holder;
	private Rigidbody theBody;

	float startTime;

	void Start () {
		startTime = Time.time;
		theBody = GetComponent<Rigidbody> ();
	}

	void Update () {
		
		if (Time.time > startTime + lifeTime) {
			Destroy (gameObject);
		}
		else{
			theBody.velocity = transform.forward * 5f * (-1);
		}
	}

	void OnTriggerEnter(Collider other) {
		GameObject player = other.GetComponent<GameObject> ();

		if (player != null) {
			//player.ApplyDamage (damage);
			Destroy (gameObject);
		}
		
		if (other.gameObject.CompareTag("Player")){
			Destroy (gameObject);
		}

	}
}

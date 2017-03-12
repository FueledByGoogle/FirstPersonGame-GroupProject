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
			characterHit.takeDamage (damage);
		}

		//TODO: For some reason setting trigger to false even after calling take damage
		//negates the damage taken
//		arrowCollider.isTrigger = false;	//So arrow doesn't just fly through walls
		Destroy (gameObject, 4f); 
	}

}

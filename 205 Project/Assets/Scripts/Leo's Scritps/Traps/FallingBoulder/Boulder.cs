using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour {
	
	private bool collided;

	public float damage = 3;
	public Collider boulderCollider;

	public float timer;
	public float timeBeforeInactive = 2f;

	void Start () {
		collided = false;
	}

	void Update () {
		if (Time.time > timer && gameObject.activeSelf && collided) {
			this.gameObject.SetActive (false);
		}
	}

	void OnTriggerEnter (Collider col) {

		/*Need to use transform.root because hitting the character in places
		* where the script isn't placed will return a null unless we traverse to where
		* the script is located. In this the Player gameobject
		*/
		Character characterHit = col.gameObject.transform.root.GetComponent<Character> ();

		if (characterHit != null && boulderCollider.isTrigger == true) {
			characterHit.TakeDamage (damage);
		}

		boulderCollider.isTrigger = false;
	}

	void OnCollisionEnter (Collision col) {
		collided = true;
		timer = Time.time + timeBeforeInactive;
	}


}

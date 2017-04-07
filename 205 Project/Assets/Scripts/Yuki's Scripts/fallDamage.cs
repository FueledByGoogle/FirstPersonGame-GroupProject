using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallDamage : MonoBehaviour {

	public GameObject spawnPoint;
	public float damage;
	// Use this for initialization
	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.transform.root.CompareTag ("Player")) {
			col.transform = spawnPoint.transform;
			GameObject player = find
			Character fallDamage = player.transform.root.GetComponent<Character> ();
			fallDamage.TakeDamage (damage);
			player.transform.position = spawnPoint.transform.position;
		}
	}
}

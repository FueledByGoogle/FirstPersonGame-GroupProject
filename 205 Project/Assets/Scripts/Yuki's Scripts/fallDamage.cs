using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallDamage : MonoBehaviour {

	public GameObject spawnPoint;
	public float damage;
	private GameObject player;

	void Start () {
		player = GameObject.Find ("MyCustomPlayer");
	}
	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.transform.root.CompareTag ("Player")) {
			player.transform.position = spawnPoint.transform.position;
			Character hit = player.transform.root.GetComponent<Character> ();
			hit.TakeDamage (damage);
		}
	}
}

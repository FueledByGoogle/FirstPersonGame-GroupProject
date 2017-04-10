using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	public GameObject healthBar;
	private GameObject playerPos;

	void Start () {
		playerPos = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update() {
		if (playerPos != null && this.gameObject.transform.root.tag != "Player") {
			Vector3 dist = (playerPos.transform.position - transform.position).normalized;
			Quaternion rotation = Quaternion.LookRotation (dist);
			transform.rotation = rotation;

		}
	}

	public void SetHealth (float currHealth, float maxHealth) {
		float scaledHealth = currHealth / maxHealth;
		healthBar.transform.localScale = new Vector3 (scaledHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}
}

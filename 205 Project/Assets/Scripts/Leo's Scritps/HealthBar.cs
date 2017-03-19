using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	public GameObject healthBar;

	public void SetHealth (float currHealth, float maxHealth) {
		float scaledHealth = currHealth / maxHealth;
		healthBar.transform.localScale = new Vector3 (scaledHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}
}

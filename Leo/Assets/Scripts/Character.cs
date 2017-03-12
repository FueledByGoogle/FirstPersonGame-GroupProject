using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public const float maxHealth = 10f;
	public float health;

	public const float walkSpeed = 1.5f;
	public const float runSpeed = 5f;
	public const float turnSpeed = 120f;

	public HealthBar healthBar;	
	public Rigidbody characterRigidBody;

	void Start () {
		characterRigidBody = GetComponent<Rigidbody> ();
		health = maxHealth;
	}
	

	void Update () {
		
	}

	public void Move (float direction, bool run) {

		if (run == false) {
			transform.Translate(Vector3.forward * direction * walkSpeed * Time.deltaTime);
		} else {
			transform.Translate(Vector3.forward *direction * runSpeed * Time.deltaTime);
		}
	}

	public void Rotate (bool turnRight){
		
		if (turnRight == true) {
			transform.Rotate (0, turnSpeed * Time.deltaTime, 0);
		} else {
			transform.Rotate (0, (-1) * turnSpeed * Time.deltaTime, 0);
		}

	}

	public void takeDamage (float damage){
		health -= damage;
		if (health < 0) {
			health = 0;
		}
		healthBar.SetHealth (health, maxHealth);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public float maxHealth = 10f;
	public float health;

	public float walkSpeed = 1f;
	public const float runSpeed = 1.5f;

	public HealthBar healthBar;	
 	Rigidbody characterRigidBody;


	//Jumping
	public Transform groundCheckTransform;
	public bool isGrounded;
	public float jumpSpeed = 150f;

	void Start () {
		characterRigidBody = GetComponent<Rigidbody> ();
		health = maxHealth;
		isGrounded = true;
	}

	void FixedUpdate () {
		if (groundCheckTransform != null) {
			isGrounded = GroundCheck ();
		}
	}

	public void Jump () {
		if (Input.GetKey(KeyCode.Space) && isGrounded){
			characterRigidBody.AddForce (new Vector3 (0, jumpSpeed, 0));
		}
	}

	public void Move (float direction, bool run) {
		if (run == false) {
			transform.Translate(Vector3.forward * direction * walkSpeed * Time.deltaTime);
		} else {
			transform.Translate(Vector3.forward *direction * runSpeed * Time.deltaTime);
		}
	}

	public void TakeDamage (float damage){
		health -= damage;
		if (health < 0) {
			health = 0;
		}
		healthBar.SetHealth (health, maxHealth);
	}

	public bool GroundCheck () {
		/*We shoot a ray down with length 0.1f from the between the two feet at
		*the groundCheckTransform to see if it hits anything.
		*NOTE: make sure groundCheckTransform is close to the ground, otherwise ray will fall short of the ground
		*/
		isGrounded = Physics.Raycast (groundCheckTransform.position, Vector3.down, 0.07f);
		return isGrounded;
	}
}

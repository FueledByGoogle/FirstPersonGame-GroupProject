using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public HealthBar healthBar;	
	Rigidbody characterRigidBody;
	public Animator animator;

	public float maxHealth = 10f;
	public float health;

	//Movement
	public float walkSpeed = 1f;

	//Defense
	public Shield shield;
	public float shieldCoolDown;	//This will be set to a time in the future when the shield can be used again

	//Jumping
	public Transform groundCheckTransform;
	public bool isGrounded;
	public float groundCheckDistance;
	public float jumpSpeed = 150f;

	void Start () {
		animator = GetComponent<Animator> ();
		characterRigidBody = GetComponent<Rigidbody> ();
		health = maxHealth;
		shieldCoolDown = 0;
	}

	void FixedUpdate () {
		if (groundCheckTransform != null) {
			isGrounded = GroundCheck ();
		}
	}

	/* For moving and jumping we first modify the vector we want to modify in localSpace,
	 * and then convert it back into world space.
	 * You can't simply use transform.up because if you were moving forward it would set
	 * your horizontal movement to zero
	*/
	public void Jump () {
		Vector3 localVelocity = new Vector3 (0, 0, 0);

		if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
			localVelocity = transform.InverseTransformDirection (characterRigidBody.velocity);
			localVelocity.y = jumpSpeed;
			characterRigidBody.velocity = transform.TransformDirection (localVelocity);
		}
	}

	public void Move (bool forward) {
		//movement using physics
		Vector3 localVelocity = new Vector3 (0, 0, 0);

		if (forward) {
			localVelocity = transform.InverseTransformDirection (characterRigidBody.velocity);
			localVelocity.z = walkSpeed;
			characterRigidBody.velocity = transform.TransformDirection (localVelocity);
		} else {
			localVelocity = transform.InverseTransformDirection (characterRigidBody.velocity);
			localVelocity.z = -walkSpeed;
			characterRigidBody.velocity = transform.TransformDirection (localVelocity);
		}

	}

	public void Strafe(bool left) {

		Vector3 localVelocity = new Vector3(0, 0, 0);

		if (left) {
			localVelocity = transform.InverseTransformDirection (characterRigidBody.velocity);

			localVelocity.x = -walkSpeed;

			if (localVelocity.z != 0f) {	//we need to multiply forward and side speed by 0.7071 otherwise player can cheat and move faster
				localVelocity.x = localVelocity.x * 0.7071f;									//TODO: Check if multiplying by 0.7071 is the almost equivalent to normalizing
				localVelocity.z = localVelocity.z * 0.7071f;
			}
			characterRigidBody.velocity = transform.TransformDirection (localVelocity);

		} else {
			localVelocity = transform.InverseTransformDirection (characterRigidBody.velocity);
			localVelocity.x = walkSpeed;

			if (localVelocity.z != 0f) {
				localVelocity.x = localVelocity.x * 0.7071f;
				localVelocity.z = localVelocity.z * 0.7071f;
			}
			characterRigidBody.velocity = transform.TransformDirection (localVelocity);
		}
	}

	public void TakeDamage (float damage){

		if (shield != null && animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Hold_Defense")) {
			//Check if shield is strong enough to block damage
			if (shield.TakeDamage (damage)) {
				//setting health
				health -= damage;
				if (health < 0) {
					health = 0;
				}
				healthBar.SetHealth (health, maxHealth);
				//setting shield animation
				if (animator != null) {
					animator.SetBool ("Defense_Broken", true);
					animator.SetBool ("Shield_Up", false);
				}
				shieldCoolDown = Time.time + shield.shieldCoolDown;

			} else {
				shieldCoolDown = Time.time + shield.shieldCoolDown;
				animator.SetBool ("Shield_Up", false);
				animator.SetBool ("Defense_Broken", false);
			}

		} else {
			health -= damage;
			if (health < 0) {
				health = 0;
			}
			healthBar.SetHealth (health, maxHealth);
		}
	}

	public void GainHealth (float healAmount) {
		health += healAmount;
		if (health > maxHealth) {
			health = maxHealth;
		}
		healthBar.SetHealth (health, maxHealth);
		/*IMPORTANT CHANGE!!!!!!
		* Added healthBar.SetHealth (health, maxHealth); where there was none
		*/
		
		
	}

	public bool GroundCheck () {
		/*We shoot a ray down with length 0.1f from the between the two feet at
		* the groundCheckTransform to see if it hits anything.
		* NOTE: make sure groundCheckTransform is close to the ground, otherwise ray will fall short of the ground
		*/
		isGrounded = Physics.Raycast (groundCheckTransform.position, Vector3.down, groundCheckDistance);
		return isGrounded;
	}
}

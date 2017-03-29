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
	public const float runSpeed = 1.5f;

	//Defense
	public Shield shield;
	public float shieldTempTime;	//This will be set to a time in the future when the shield can be used again

	//Jumping
	public Transform groundCheckTransform;
	public bool isGrounded;
	public float jumpSpeed = 150f;

	void Start () {
		animator = GetComponent<Animator> ();
		characterRigidBody = GetComponent<Rigidbody> ();
		health = maxHealth;
		shieldTempTime = 0;
	}

	void FixedUpdate () {
		if (groundCheckTransform != null) {
			isGrounded = GroundCheck ();
		}
	}

	public void Jump () {
		if (Input.GetKeyDown (KeyCode.Space) && isGrounded){
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
				shieldTempTime = Time.time + shield.shieldCoolDown;

			} else {
				shieldTempTime = Time.time + shield.shieldCoolDown;
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
	}

	public bool GroundCheck () {
		/*We shoot a ray down with length 0.1f from the between the two feet at
		* the groundCheckTransform to see if it hits anything.
		* NOTE: make sure groundCheckTransform is close to the ground, otherwise ray will fall short of the ground
		*/
		isGrounded = Physics.Raycast (groundCheckTransform.position, Vector3.down, 0.1f);
		return isGrounded;
	}
}

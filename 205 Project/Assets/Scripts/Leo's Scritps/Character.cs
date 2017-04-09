using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public HealthBar healthBar;
	public Rigidbody rigidBody;
	public Animator anim;

	public float maxHealth = 10f;
	public float health;

	//Movement
	public float walkSpeed = 1f;

	//Defense
	public Shield shield;
	public float shieldCoolDownCounter;	//This will be set to a time in the future when the shield can be used again


	//Jumping
	public Transform groundCheckTransformLeft;
	public Transform groundCheckTransformRight;
	public Transform groundCheckTransformMiddle;
	public bool isGrounded;
	public float groundCheckDistance;
	public float jumpSpeed = 150f;

	public EnemyAI enemy;
	public PlayerController player;

	void Start () {
		anim = GetComponent<Animator> ();
		rigidBody = GetComponent<Rigidbody> ();
		player = GameObject.Find ("MyCustomPlayer").GetComponent<PlayerController> ();
		health = maxHealth;
		shieldCoolDownCounter = 0;

	}


	void FixedUpdate () {
		if (groundCheckTransformLeft != null && groundCheckTransformRight != null && groundCheckTransformMiddle != null) {
			isGrounded = GroundCheck ();
		}
	}

	/* For moving and jumping we first modify the vector we want to modify in localSpace,
	 * and then convert it back into world space.
	 * You can't simply use transform.up because if you were moving forward it would set
	 * your horizontal movement to zero. So we use rigidBody.velocity so it retains current velocities
	*/
	public void Jump () {
		Vector3 localVelocity = new Vector3 (0, 0, 0);

		if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
			localVelocity = transform.InverseTransformDirection (rigidBody.velocity);
			localVelocity.y = jumpSpeed;
			rigidBody.velocity = transform.TransformDirection (localVelocity);
		}
	}

	public void Move (bool forward) {
		
		Vector3 localVelocity = new Vector3 (0, 0, 0);

		if (forward) {
			localVelocity = transform.InverseTransformDirection (rigidBody.velocity);
			localVelocity.z = walkSpeed;
			rigidBody.velocity = transform.TransformDirection (localVelocity);

		} else {
			localVelocity = transform.InverseTransformDirection (rigidBody.velocity);
			localVelocity.z = -walkSpeed;
			rigidBody.velocity = transform.TransformDirection (localVelocity);
		}

	}

	public void Strafe(bool left) {

		Vector3 localVelocity = new Vector3(0, 0, 0);

		if (left) {
			localVelocity = transform.InverseTransformDirection (rigidBody.velocity);
			localVelocity.x = -walkSpeed;

			if (localVelocity.z != 0f) {	//we need to multiply forward and side speed by 0.7071 otherwise player can cheat and move faster
				localVelocity.x = localVelocity.x * 0.7071f;									//TODO: Check if multiplying by 0.7071 is the almost equivalent to normalizing
				localVelocity.z = localVelocity.z * 0.7071f;
			}
			rigidBody.velocity = transform.TransformDirection (localVelocity);

		} else {
			localVelocity = transform.InverseTransformDirection (rigidBody.velocity);
			localVelocity.x = walkSpeed;

			if (localVelocity.z != 0f) {
				localVelocity.x = localVelocity.x * 0.7071f;
				localVelocity.z = localVelocity.z * 0.7071f;
			}
			rigidBody.velocity = transform.TransformDirection (localVelocity);
		}
	}

	public void TakeDamage (float damage) {


		//TODO: find way to tell if shield is currently colliding
		if (shield != null && anim.GetCurrentAnimatorStateInfo(0).IsName("Hold_Defense")) {

			if (shield.TakeDamage (damage)) {	//take damage
				//setting health
				health -= damage;
				if (health < 0) {
					health = 0;
				}
				healthBar.SetHealth (health, maxHealth);
				//setting shield animation
				if (anim != null) {
					anim.SetBool ("Defense_Broken", true);
					anim.SetBool ("Shield_Up", false);
				}
				shieldCoolDownCounter = Time.time + shield.shieldCoolDown;

			} else {							//ignore damage
				shieldCoolDownCounter = Time.time + shield.shieldCoolDown;
				anim.SetBool ("Shield_Up", false);
				anim.SetBool ("Defense_Broken", false);
			}

		} else {

			health -= damage;
			if (health < 0) {
				health = 0;
			}
			healthBar.SetHealth (health, maxHealth);

			//for the npc, if npc isn't attacking then trigger take damage animation
			if (enemy != null) {
				if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Enemy_Attack")) {
					anim.SetTrigger ("TakingDamage");
				}
			}
		}
	}

	public void GainHealth (float healAmount) {
		health += healAmount;
		if (health > maxHealth) {
			health = maxHealth;
		}
		healthBar.SetHealth (health, maxHealth);
	}

	public bool GroundCheck () {
		/*We shoot a ray down with length 0.1f from the between the two feet at
		* the groundCheckTransform to see if it hits anything.
		* NOTE: make sure groundCheckTransform is close to the ground, otherwise ray will fall short of the ground
		*/
		if (Physics.Raycast (groundCheckTransformLeft.position, Vector3.down, groundCheckDistance) ||
			Physics.Raycast (groundCheckTransformRight.position, Vector3.down, groundCheckDistance)||
			Physics.Raycast (groundCheckTransformMiddle.position, Vector3.down, groundCheckDistance)) {
			isGrounded = true;
		} else {
			isGrounded = false;
		}
		return isGrounded;
	}
}

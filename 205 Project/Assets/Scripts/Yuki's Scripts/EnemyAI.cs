using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

	public Transform[] points;
	public CombatRoomManager combatRoomManager; //on death -1 enemy in room
	//player
	private Transform playerPos;
	private GameObject player;
	//NPC properties
	public Character character;
	public Sword sword;
	//navigation
	private int destPoint = 0;
	private NavMeshAgent agent;
	private float distToPlayer;
    public float attackDist;
    public float fieldOfViewRange;				//field of view in degrees
    public float RotationSpeed;
    public float viewRange;
    Vector3 inFront;

    public bool inLineSight;
	bool canAttack;
	float attackCooldown;
    Vector3 rayDir;

	public float tempHP;

	void Start() {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		character = GetComponent<Character> ();
        player = GameObject.Find("MyCustomPlayer");
		combatRoomManager = GameObject.Find ("CombatRoomManager").GetComponent<CombatRoomManager> ();
        agent.autoBraking = false;
		attackCooldown = 3f;
		canAttack = true;
		tempHP = character.health;
		inLineSight = false;

		GotoNextPoint ();
    }

	void Update() {

		//print (distToPlayer);

		if (agent.enabled) {

			if(!canAttack) {
				attackCooldown -= Time.deltaTime;
				if(attackCooldown <= 0){
					attackCooldown = 3f;
					canAttack = true;
				}
			}
			if (!character.anim.GetCurrentAnimatorStateInfo (0).IsName ("Enemy_Attack")) {
				sword.hasCollided = false;
			}
			// Choose the next destination point when the agent gets
			// close to the current one.
			if (agent.remainingDistance < 0.5f && !inLineSight) {
				GotoNextPoint();
			}
				
			//Death
			if (character.health <= 0 && character.anim.enabled) {
				combatRoomManager.numOfEnemies -= 1;
				character.anim.Stop ();	  //have to stop animation after player head detaches otherwise buggy
				character.anim.Rebind (); //then we rebind so the animation plays without the head
				character.anim.enabled = false;

				character.rigidBody.AddForce (new Vector3 (100f, 0f, 0f)); //so body falls down
				agent.enabled = false;	//disable navigation
			}
		}

		if (character.health < character.maxHealth) {
			inLineSight = true;
		}

		if (Time.time >= character.shieldCoolDownCounter && inLineSight) {
			if (character.player.character.anim.GetCurrentAnimatorStateInfo (0).IsName ("Melee_Attack") && 
				distToPlayer <= attackDist																) {

				character.anim.SetBool ("Shield_Up", true);

			} else if (character.player.usingBow) {
				character.anim.SetBool ("Shield_Up", true);
			} else {
				character.anim.SetBool ("Shield_Up", false);
			}
		}

	}   

	void FixedUpdate() {
		RaycastHit hit;

		if (agent.enabled) {
			distToPlayer = Vector3.Distance(player.transform.position, transform.position);
			rayDir = player.transform.position - transform.position;

			if ((distToPlayer < viewRange) 										  && 
				(Vector3.Angle(rayDir, transform.forward) < fieldOfViewRange) 	  &&
				(Physics.Raycast (transform.position, rayDir, out hit, viewRange) &&
					hit.transform.tag == "Player")										) {

				inLineSight = true;
			}	

			if (inLineSight || distToPlayer < 1f) {
				attackPlayer ();
			}
		}
	}

    void GotoNextPoint() {
        // Returns if no points have been set up
		if (points.Length == 0) {
			return;
		}
        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // going back to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    void attackPlayer () {

        if (distToPlayer >= attackDist) {
            agent.Resume();
            agent.destination = player.transform.position;

        } else {
			agent.Stop ();
            //find the vector pointing from our position to the target
			Vector3 direction = rayDir.normalized;

            //create the rotation we need to be in to look at the target
			Quaternion lookRotation = Quaternion.LookRotation(direction);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
            transform.rotation = Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0); //locks z & x rotations

			if (canAttack) {
				canAttack = false;
				character.anim.SetTrigger ("isAttacking");
			}

        }
  
    }		

    


}

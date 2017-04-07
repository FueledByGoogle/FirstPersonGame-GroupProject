﻿using System.Collections;
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
	private Animator anim;
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

    bool inLineSight;
	bool canAttack;
	float attackCooldown;
    Vector3 rayDir;


	void Start() {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		character = GetComponent<Character> ();
        player = GameObject.Find("MyCustomPlayer");
		anim = GetComponent<Animator> ();
        agent.autoBraking = false;
		attackCooldown = 3f;
		canAttack = true;
		//GotoNextPoint();
    }


    void GotoNextPoint() {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;
		
        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    void attackPlayer() {
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
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); //locks z & x rotations

			if (canAttack) {
				canAttack = false;
				anim.SetTrigger ("isAttacking");
			}

        }
  
    }		

    void FixedUpdate() {
		RaycastHit hit;

        distToPlayer = Vector3.Distance(player.transform.position, transform.position);
        rayDir = player.transform.position - transform.position;

		if ((distToPlayer < viewRange) 										  && 
			(Vector3.Angle(rayDir, transform.forward) < fieldOfViewRange) 	  &&
			(Physics.Raycast (transform.position, rayDir, out hit, viewRange) &&
			hit.transform.tag == "Player")) {

				inLineSight = true;
        }	
    }

	void Update() {
		if (agent.enabled) {
			
			if(!canAttack) {
				attackCooldown -= Time.deltaTime;
				if(attackCooldown <= 0){
					attackCooldown = 3f;
					canAttack = true;
				}
			}
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Enemy_Attack")) {
				sword.hasCollided = false;
			}
			// Choose the next destination point when the agent gets
			// close to the current one.
			if (agent.remainingDistance < 0.5f && !inLineSight) {
				GotoNextPoint();
			}

			if (inLineSight || distToPlayer < 2.2f)
				attackPlayer();
			
			//DEATH
			if (character.health <= 0 && anim.enabled) {
				combatRoomManager.numOfEnemies -= 1;
				anim.Stop ();
				anim.Rebind ();
				anim.enabled = false;
				agent.enabled = false;
			}
		}

		if (character.health < character.maxHealth) {
			inLineSight = true;
		}



    }   
}

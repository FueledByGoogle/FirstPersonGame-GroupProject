using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    private Transform playerPos;
    private int destPoint = 0;
    private NavMeshAgent agent;
	private Animator anim;
	private GameObject player;

	public Transform[] points;
	public Character character;
	public Sword sword;

    public float attackDist;
    public float fieldOfViewRange;				//field of view in degrees
    public float RotationSpeed;
    public float viewRange;
    private float distToPlayer;
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
			if(canAttack) {
				//Debug.Log("Enemy is attacking");
				canAttack = false;
				anim.SetTrigger ("isAttacking");
			}

        }
  
    }		

    void FixedUpdate() {
		RaycastHit hit;

        distToPlayer = Vector3.Distance(player.transform.position, transform.position);
        rayDir = player.transform.position - transform.position;

        if (distToPlayer < viewRange) {
            if (Vector3.Angle(rayDir, transform.forward) < fieldOfViewRange) {
				if (Physics.Raycast (transform.position, rayDir, out hit, viewRange)) {
					if (hit.transform.tag == "Player") {
						inLineSight = true;					}
				}
//				Debug.DrawLine (transform.position, rayDir, Color.white);


            }

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

			/*TODO: Actual enemy death*/
			if (character.health <= 0) {
//				this.gameObject.SetActive (false);
				agent.enabled = false;
			}
		}

		if (character.health < character.maxHealth) {
			inLineSight = true;
		}


    }   
}

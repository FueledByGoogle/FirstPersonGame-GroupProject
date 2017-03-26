using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public Transform[] points;
    private Transform playerPos;
    private int destPoint = 0;
    private UnityEngine.AI.NavMeshAgent agent;

    public GameObject player;
	private Animator anim;
    public float attackDist;
    public float fieldOfViewRange;
    public float RotationSpeed;
    public float viewRange;
    private float distToPlayer;
    Vector3 inFront;

    bool inLineSight;
	bool canAttack;
	float attackCooldown;
    Vector3 rayDir;


    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("MyCustomPlayer");
		anim = GetComponent<Animator>();
        agent.autoBraking = false;
		attackCooldown = 3f;
		canAttack = true;

        //GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    void attackPlayer()
    {
        if (distToPlayer >= attackDist)
        {
            agent.Resume();
            agent.destination = player.transform.position;

        }
        else
        {
			agent.Stop ();
            //find the vector pointing from our position to the target
            var direction = rayDir.normalized;

            //create the rotation we need to be in to look at the target
            var lookRotation = Quaternion.LookRotation(direction);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); //locks z & x rotations
			if(canAttack){
				//Debug.Log("Enemy is attacking");
				canAttack = false;
				anim.SetTrigger ("isAttacking");
			}

        }
  
    }		

    void FixedUpdate()
    {

		Debug.DrawLine (transform.position, rayDir, Color.red);
		RaycastHit hit;



        distToPlayer = Vector3.Distance(player.transform.position, transform.position);
        rayDir = player.transform.position - transform.position;

        if (distToPlayer < viewRange) {
            if (Vector3.Angle(rayDir, transform.forward) < fieldOfViewRange)
            {
				if (Physics.Raycast (transform.position, rayDir, out hit, viewRange)) {
					if (hit.transform.tag == "Player") {
						print ("see player");
						inLineSight = true;
					}
				}
				Debug.DrawLine (transform.position, rayDir, Color.red);


            }

        }
    }

    void Update()
    {
		if(!canAttack){
			attackCooldown -= Time.deltaTime;
			if(attackCooldown <= 0){
				attackCooldown = 3f;
				canAttack = true;
			}
		}
        // Choose the next destination point when the agent gets
        // close to the current one.
		if (agent.remainingDistance < 0.5f && !inLineSight) {
			GotoNextPoint();
		}
			

        if (inLineSight || distToPlayer < 2.2f)
            attackPlayer();
    }   
}

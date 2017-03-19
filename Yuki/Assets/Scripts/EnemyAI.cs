using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public Transform[] points;
    public Transform playerPos;
    private int destPoint = 0;
    private UnityEngine.AI.NavMeshAgent agent;

    public GameObject player;
    public float attackDist;
    public float fieldOfViewRange;
    public float RotationSpeed;
    public float viewRange;
    private float distToPlayer;
    Vector3 inFront;
    private RaycastHit hit;

    bool inLineSight;
    Vector3 rayDir;


    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("Player");

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

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
        if (distToPlayer > attackDist)
        {
            agent.Resume();
            agent.destination = player.transform.position;

        }
        else
        {
            agent.Stop();

            //find the vector pointing from our position to the target
             var direction = rayDir.normalized;

            //create the rotation we need to be in to look at the target
            var lookRotation = Quaternion.LookRotation(direction);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); //locks z & y rotations

            Debug.Log("Enemy is attacking");
        }
  
    }

    void FixedUpdate()
    {
        distToPlayer = Vector3.Distance(player.transform.position, transform.position);
        rayDir = player.transform.position - transform.position;

        if (distToPlayer < viewRange + 1) {
            if (Vector3.Angle(rayDir, transform.forward) < fieldOfViewRange)
            {
                Physics.Raycast(transform.position, rayDir, out hit, viewRange);
                if (hit.transform.tag == "Player")
                    inLineSight = true;
            }

        }
    }

    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (agent.remainingDistance < 0.5f && !inLineSight)
            GotoNextPoint();

        if (inLineSight || distToPlayer < 2.2f)
            attackPlayer();
    }   
}

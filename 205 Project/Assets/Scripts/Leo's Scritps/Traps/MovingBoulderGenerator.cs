using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBoulderGenerator : MonoBehaviour {

	
	private Vector3 tempVector;
	private Vector3 originWorld;
	private float tempTime;

	public GameObject boulder;
	public Transform spawnTransform;
	public GameObject warning;
	//Time related
	public float timeToChangePos;
	public int moveDistance;


	void Start () {
		tempTime = 0;
		originWorld = transform.position;
	}

	void Update () {
		if (Time.time > tempTime) {
			float newPosX = Random.Range (originWorld.x - moveDistance, originWorld.x + moveDistance);
//			float newPosZ = Random.Range (originWorld.z - moveDistance, originWorld.z + moveDistance);
//			transform.position = new Vector3 (newPosX, transform.position.y, newPosZ);
			transform.position = new Vector3 (newPosX, transform.position.y,transform.position.z);
			GameObject spawnedBoulder = Instantiate (boulder, spawnTransform.position, spawnTransform.rotation) as GameObject;
//			Boulder spawnedBoulderScript = spawnedBoulder.GetComponentInChildren<Boulder> ();
//			spawnedBoulderScript.timeBeforeInactive = 1f;
			Destroy(spawnedBoulder, 2f);
			tempTime = Time.time + timeToChangePos;
		}

	}
}

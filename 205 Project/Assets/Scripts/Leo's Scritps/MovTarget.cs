using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovTarget : MonoBehaviour {

	private Vector3 originWorld;
	private float tempTime;
	private Vector3 tempVector;

	//Time related
	public float timeToChangePos;
	public int moveDistance;


	void Start () {
		tempTime = 0;
		originWorld = transform.position;
	}

	void Update () {
		if (Time.time > tempTime) {
			float temp = Random.Range (originWorld.x - moveDistance, originWorld.x + moveDistance);
			transform.position = new Vector3 (temp, transform.position.y, transform.position.z);

			tempTime = Time.time + timeToChangePos;
		}
//		Debug.DrawLine(originWorld, transform.position, Color.red);
	}

	void OnTriggerEnter () {
		this.gameObject.SetActive (false);
	}


		

		



}

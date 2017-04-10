using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitDistance : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int dist = Random.Range (1, 4);
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + (1.5f * dist));
	}

}

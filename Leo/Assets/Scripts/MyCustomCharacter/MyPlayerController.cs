using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerController : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
		if (Input.GetMouseButton (0)) {
			Attack ();
		}
	}

	void Attack () {
	}
}

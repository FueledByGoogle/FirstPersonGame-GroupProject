using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSectionSpawnManager : MonoBehaviour {
	
	public GameObject[] Traps;
	public int numOfSections = 3;

	Transform nextSpawnTransform;

	void Start () {

		GameObject[] trapsToInstantiate = new GameObject[numOfSections];
	
		for (int i = 0; i < numOfSections; i++) {
			if (i == 0) {
				trapsToInstantiate [i] = Instantiate (Traps [Random.Range (0, Traps.Length)], Vector3.zero, new Quaternion (0, 0, 0, 0));
			} else {

				Vector3 tmp = trapsToInstantiate [i - 1].transform.FindChild ("NextSpawnTransform").position;
//				tmp.y = tmp.y - 0.05f; //this is to offset the next trap platform to be a tiny bit below so player doesn't gets stuck between colliders
				trapsToInstantiate [i] = Instantiate (Traps [Random.Range (0, Traps.Length)], tmp, new Quaternion (0, 0, 0, 0));

			}
		}
	}

}

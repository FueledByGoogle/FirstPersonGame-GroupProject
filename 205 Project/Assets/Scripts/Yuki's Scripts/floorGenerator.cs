using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorGenerator : MonoBehaviour {

	public GameObject entrance;
	public GameObject exit;
	public GameObject floorTile;

	float dist2Exit;
	float numOfFloors;
	//float[] pointLoc;
	// Use this for initialization
	void Start () {
		dist2Exit = Vector3.Distance (entrance.transform.position, exit.transform.position);
		numOfFloors = Mathf.Ceil(dist2Exit / 1.5f); //1.5f is the length of the floor tiles
		generateFloors();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void generateFloors(){
		generatePoints();

	}

	void generatePoints(){
		for (int i = 0; i < numOfFloors; i++) {
			float temp = Random.Range (-10f,10f)/10;
			Debug.Log (temp);
			Instantiate (floorTile, new Vector3 (temp, -0.1f, -2 + i * 1.5f),transform.rotation);
		}
	}
}

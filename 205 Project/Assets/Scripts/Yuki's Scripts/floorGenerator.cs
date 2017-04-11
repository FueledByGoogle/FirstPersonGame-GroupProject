using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class floorGenerator : MonoBehaviour {

	public GameObject entrance;
	public GameObject exit;
	public GameObject floorTile;
	public GameObject target;
	public Door door;
	public GameObject[] targetSpawnPos;

	float dist2Exit;
	float numOfFloors;
	float[] floorLoc;

	private int numTargets;
	// Use this for initialization
	void Start () {
		dist2Exit = Vector3.Distance (entrance.transform.position, exit.transform.position);
		numOfFloors = Mathf.Floor(dist2Exit / 1.5f); //1.5f is the length of the floor tiles
		floorLoc = new float[(int) numOfFloors];
		generateFloors();
		generateTargets();
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.P)) {
			SceneManager.LoadScene (3, LoadSceneMode.Single);
		}
	}

	void generateFloors(){
		generatePoints();
		for(int j = 0; j < numOfFloors; j++){
			Instantiate (floorTile, new Vector3 (floorLoc [j], -0.1f, -2 + j * 1.5f),transform.rotation);
		}
		exit.transform.position = new Vector3 (floorLoc[(int) numOfFloors - 1], exit.transform.position.y, exit.transform.position.z);

	}

	void generatePoints(){
		float[] runAvg = {0,0,0};
		float avg = 0;
		for (int i = 0; i < numOfFloors; i++) {
			for(int j = 1; j < 3; j++){
				runAvg [j] = Random.Range (-10f,10f)/10;
			}
			for(int k = 0; k < 3; k++){
				if(i == numOfFloors-1 && k == 2){
					runAvg [k] = 0;
				}
				avg = avg + runAvg [k];
			}
			floorLoc[i] = avg / 3;
			runAvg [0] = runAvg [2];


		}
	}

	void generateTargets(){
		numTargets = Random.Range (2, 7);
		int pos = 0;
		for(int i = 0; i < numTargets; i++){			
			Instantiate (target, targetSpawnPos[pos].transform.position, targetSpawnPos[pos].transform.rotation);
			pos++;
		}
	}

	public void targetHit(){
		numTargets--;
		if(numTargets <= 0){
			door.cleared = true;
		}
	}
}

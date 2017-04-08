using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLevelMaze : MonoBehaviour {

	
	public GameObject roof;
	public GameObject[] trapMazeSettings;


	// Use this for initialization
	void Start () {
		//Random.InitState(75);
		float rand = Random.value;
		int index = 0;
		if(rand > 0.7f){
			index = 2;
		}
		else if(rand < 0.3f){
			index = 0;
		}
		else{
			index = 1;
		}
		trapMazeSettings[index].SetActive(true);
		roof.SetActive(true);
	}
	

}

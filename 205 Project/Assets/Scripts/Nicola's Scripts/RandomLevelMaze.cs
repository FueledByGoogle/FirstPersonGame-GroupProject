using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomLevelMaze : MonoBehaviour {

	
	public GameObject roof;
	public GameObject[] trapMazeSettings;

	// Use this for initialization
	void Start () {
	//	Random.InitState(20);
		int rand =  Random.Range (0, trapMazeSettings.Length);
		trapMazeSettings[rand].SetActive(true);
		roof.SetActive(true);
	}
	
	void Update(){
		if (Input.GetKeyDown(KeyCode.P)) {
			SceneManager.LoadScene (4, LoadSceneMode.Single);
		}
	}
}

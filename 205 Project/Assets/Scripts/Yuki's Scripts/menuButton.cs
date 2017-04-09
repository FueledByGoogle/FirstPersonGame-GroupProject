using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuButton : MonoBehaviour {

	public void LoadScene(int level){
		if(level == -1){
			Application.Quit ();
		}
		else {
			SceneManager.LoadScene (level, LoadSceneMode.Single);
		}
	}
		
}

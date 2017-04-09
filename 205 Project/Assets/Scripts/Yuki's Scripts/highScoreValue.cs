using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highScoreValue : MonoBehaviour {

	public Text score;
	// Use this for initialization
	void Start () {
		score.text = ("Highscore: " + PlayerPrefs.GetInt ("highscore", 0));
	}

}

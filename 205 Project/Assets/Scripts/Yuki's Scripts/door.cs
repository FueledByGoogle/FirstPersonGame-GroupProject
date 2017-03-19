using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class door : MonoBehaviour
{

	public Text enterText;
	public int nextRoom;
	public bool cleared;
	public GameObject lvlMan;

	// Use this for initialization
	void Start ()
	{
		enterText.enabled = false;
	}

	void Update ()
	{
		if (enterText.enabled) {
			if (Input.GetKey (KeyCode.E)) {
				DontDestroyOnLoad (lvlMan.transform.gameObject);
				SceneManager.LoadScene (nextRoom, LoadSceneMode.Single);
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (cleared && other.CompareTag ("Player")) {
			enterText.enabled = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (cleared && other.CompareTag ("Player")) {
			enterText.enabled = false;
		}
	}

	public void childRoom ()
	{

	}
}

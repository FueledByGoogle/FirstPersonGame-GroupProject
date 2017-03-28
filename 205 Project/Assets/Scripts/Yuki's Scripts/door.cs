using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class door : MonoBehaviour
{

	public GameObject enterText;
	public int nextRoom;
	public bool cleared;
	public GameObject lvlMan;
	//public int roomNum;

	// Use this for initialization
	void Start ()
	{
		//lvlMan = GameObject.Find("lvlman");
	}

	void Update ()
	{
		if(enterText != null){
			if (enterText.activeSelf) {
				if (Input.GetKey (KeyCode.E)) {
					DontDestroyOnLoad (lvlMan.transform.gameObject);
					//lvlMan.next
					SceneManager.LoadScene (nextRoom, LoadSceneMode.Single);
				}
			}
		}

	}

	void OnTriggerEnter (Collider other)
	{
		if (cleared && other.gameObject.transform.root.CompareTag ("Player")) {
			enterText.SetActive (true);
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (cleared && other.gameObject.transform.root.CompareTag ("Player")) {
			enterText.SetActive (false);
		}
	}

	public void childRoom ()
	{

	}
}

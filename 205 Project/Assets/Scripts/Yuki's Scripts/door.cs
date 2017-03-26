using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class door : MonoBehaviour
{

	public GameObject enterText;
	//public GameObject player;
	public int nextRoom;
	public bool cleared;
	public GameObject lvlMan;

	// Use this for initialization
	void Start ()
	{

	}

	void Update ()
	{
		if(enterText != null){
			if (enterText.activeSelf) {
				if (Input.GetKey (KeyCode.E)) {
					DontDestroyOnLoad (lvlMan.transform.gameObject);
					//DontDestroyOnLoad (player.transform.gameObject);
					//player.transform.position = new Vector3 (-2.45f, 2f, -0.36f);
					//player.transform.rotation = new Quaternion (0, 90, 0, 0);
					SceneManager.LoadScene (nextRoom, LoadSceneMode.Single);
				}
			}
		}

	}

	void OnCollisionEnter (Collision other)
	{
		if (cleared && other.gameObject.transform.root.CompareTag ("Player")) {
			enterText.SetActive (true);
		}
	}

	void OnCollisionExit (Collision other)
	{
		if (cleared && other.gameObject.transform.root.CompareTag ("Player")) {
			enterText.SetActive (false);
		}
	}

	public void childRoom ()
	{

	}
}

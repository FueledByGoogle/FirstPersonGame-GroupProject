using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

	public GameObject enterText;
	public PlayerController player;
	public int nextRoom;
	public bool cleared;

	void Start ()
	{
		nextRoom = Random.Range (1, 3);
		player = GameObject.Find ("MyCustomPlayer").GetComponent<PlayerController> ();
	}	

	void Update ()
	{
		if (enterText != null){
			if (enterText.activeSelf) {
				if (Input.GetKey (KeyCode.E)) {
					SceneManager.LoadScene (nextRoom, LoadSceneMode.Single);
					player.roomsCleared += 1;
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

}

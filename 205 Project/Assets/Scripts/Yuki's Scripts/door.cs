using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

	private PlayerController player;
	private int randomRoom;
	public GameObject enterText;
	public int nextRoomPicked;
	public bool pickNextRoom;
	public bool cleared;

	void Start () {
		randomRoom = Random.Range (1, 5);
		player = GameObject.Find ("MyCustomPlayer").GetComponent<PlayerController> ();
	}	

	void Update () {
		if (enterText != null && enterText.activeSelf && Input.GetKey (KeyCode.E)) {
			if (pickNextRoom) {
				SceneManager.LoadScene (nextRoomPicked, LoadSceneMode.Single);
			} else {
				SceneManager.LoadScene (randomRoom, LoadSceneMode.Single);
			}
			player.roomsCleared += 1;
			player.roomsClearedText.text = ("Rooms Cleared: " + player.roomsCleared);
		}

	}

	void OnTriggerEnter (Collider other) {
		if (cleared && other.gameObject.transform.root.CompareTag ("Player")) {
			enterText.SetActive (true);
		}
	}

	void OnTriggerExit (Collider other) {
		if (cleared && other.gameObject.transform.root.CompareTag ("Player")) {
			enterText.SetActive (false);
		}
	}

}

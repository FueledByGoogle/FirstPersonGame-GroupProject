using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignPostScript : MonoBehaviour {
	
	public GameObject backgroundPanel;
	
	public GameObject buttonPanel;
	public UnityEngine.UI.Text pressR;
	
	PlayerController player;
	
	//public int maxTextBoxes; //incase I can figure out how to run through dialogue options
	
	public UnityEngine.UI.Text reading1;
	
	bool isReadingThis;
	int numberOfTextBoxes;
	Vector3 origin;


	// Use this for initialization
	void Start () {
		isReadingThis = false;
		numberOfTextBoxes = 0;
		backgroundPanel.SetActive(false);
		pressR.enabled = false;
		reading1.enabled = false;
		buttonPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(isReadingThis == true){
			player.transform.position = origin;
			if(numberOfTextBoxes == 0){
				buttonPanel.SetActive(false);
				backgroundPanel.SetActive(true);
				reading1.enabled = true;
				numberOfTextBoxes++;
			}
			else if(Input.GetKeyDown (KeyCode.R) && numberOfTextBoxes == 1){
				reading1.enabled = false;
				isReadingThis = false;
				numberOfTextBoxes = 0;
				pressR.enabled = false;
				backgroundPanel.SetActive(false);
			}
		}
		
		if (Input.GetKeyDown (KeyCode.R) && pressR.enabled == true) { 
			pressR.enabled = false;
			isReadingThis = true;
		}
	}
	
	void OnTriggerEnter (Collider coll) {
		player = coll.gameObject.transform.root.GetComponent<PlayerController > ();
		//The following if statement enables the button prompt once in the trigger range of a sign
		if (coll.gameObject.transform.root.CompareTag("Player")){
			
			buttonPanel.SetActive(true);
			pressR.enabled = true;
			origin = player.transform.position;
		}
		
	}
	
	void OnTriggerExit (Collider coll) {
		
		//The following if statement DISABLES the button prompt once leaving the trigger of a sign
		if (coll.gameObject.transform.root.CompareTag("Player")){
			pressR.enabled = false;
			buttonPanel.SetActive(false);
			//player = null;
		}
		
	}
}

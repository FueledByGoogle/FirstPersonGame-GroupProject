using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScript : MonoBehaviour {


	public UnityEngine.UI.Text pressR;
	
	public UnityEngine.UI.Text reading1;
	public UnityEngine.UI.Text reading2;
	public UnityEngine.UI.Text reading3;
	
	PlayerController player;

	bool isReadingThis;
	int numberOfTextBoxes;


	// Use this for initialization
	void Start () {
		isReadingThis = false;
		numberOfTextBoxes = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(isReadingThis == true){
			
			if(numberOfTextBoxes == 0){
				reading1.enabled = true;
				numberOfTextBoxes++;
			}
			else if(Input.GetKeyDown (KeyCode.R) && numberOfTextBoxes == 1){
				reading1.enabled = false;
				reading2.enabled = true;
				numberOfTextBoxes++;
			}
			else if(Input.GetKeyDown (KeyCode.R) && numberOfTextBoxes == 2){
				reading2.enabled = false;
				reading3.enabled = true;
				numberOfTextBoxes++;
			}
			else if(Input.GetKeyDown (KeyCode.R) && numberOfTextBoxes == 3){
				reading3.enabled = false;
				isReadingThis = false;
				numberOfTextBoxes = 0;
				player.movementDisabled = false;
				pressR.enabled = false;
			}
		}
		
		if (Input.GetKeyDown (KeyCode.R) && pressR.enabled == true) { 
			player.movementDisabled = true;
			pressR.enabled = false;
			isReadingThis = true;
		}

		
	}
	
	
	void OnTriggerEnter (Collider coll) {

		//The following if statement enables the button prompt once in the trigger range of an NPC
		if (coll.gameObject.transform.root.CompareTag("Player")){
			player = coll.gameObject.transform.root.GetComponent<PlayerController > ();
			pressR.enabled = true;
		}
		
	}
	
	void OnTriggerExit (Collider coll) {
		
		//The following if statement DISABLES the button prompt once leaving the trigger of an NPC
		if (coll.gameObject.transform.root.CompareTag("Player")){
			pressR.enabled = false;
			player = null;
		}
		
	}
}
